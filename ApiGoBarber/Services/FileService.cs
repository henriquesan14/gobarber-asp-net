using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using ApiGoBarber.DTOs;
using ApiGoBarber.Entities;
using ApiGoBarber.Repositories.Interfaces;
using ApiGoBarber.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ApiGoBarber.Services
{
    public class FileService : IFileService
    {

        private readonly IFileRepository _fileRepository;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        private AmazonS3Client s3Client;
        private const double DURATION = 24;

        public FileService(IFileRepository fileRepository, IConfiguration configuration, IMapper mapper)
        {
            _fileRepository = fileRepository;
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<AvatarDTO> Save(IFormFile formFile)
        {
            var credentials = new BasicAWSCredentials(_configuration["AwsSettings:AccessKey"], _configuration["AwsSettings:SecretKey"]);
            s3Client = new AmazonS3Client(credentials, RegionEndpoint.SAEast1);
            string fileName = formFile.FileName;
            string objectKey = $"{_configuration["AwsSettings:FolderName"]}/{fileName}";
            string url = "";

            using (Stream fileToUpload = formFile.OpenReadStream())
            {
                var putObjectRequest = new PutObjectRequest();
                putObjectRequest.BucketName = _configuration["AwsSettings:BucketName"];
                putObjectRequest.Key = objectKey;
                putObjectRequest.InputStream = fileToUpload;
                putObjectRequest.ContentType = formFile.ContentType;

                var response = await s3Client.PutObjectAsync(putObjectRequest);

                url = GeneratePreSignedURL(objectKey);
            }
            Avatar avatarSaved = await _fileRepository.AddAsync(new Avatar {
                Name = fileName,
                Url = url
            });
            AvatarDTO dto = _mapper.Map<AvatarDTO>(avatarSaved);
            return dto;
        }

        private string GeneratePreSignedURL(string objectKey)
        {
            var request = new GetPreSignedUrlRequest
            {
                BucketName = _configuration["AwsSettings:BucketName"],
                Key = objectKey,
                Verb = HttpVerb.GET,
                Expires = DateTime.UtcNow.AddHours(DURATION)
            };

            string url = s3Client.GetPreSignedURL(request);
            return url;
        }
    }
}
