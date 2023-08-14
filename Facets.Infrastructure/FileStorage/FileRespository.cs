using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Facets.Core.Common.Dtos;
using Facets.Core.Common.Interfaces;
using Facets.SharedKernal.Exceptions;
using Facets.SharedKernal.Helpers;
using Facets.SharedKernal.Responses;

namespace Facets.Infrastructure.FileStorage;

internal sealed class FileRespository : IFileRespository
{
    private readonly BlobServiceClient _blobServiceClient;
    private const string ContentType = "application/octet-stream";

    public FileRespository(BlobServiceClient blobServiceClient)
    {
        _blobServiceClient = blobServiceClient;
    }

    public async Task<ResponseResult> DeleteFile(string containerName, string blobName, CancellationToken token)
    {
        var blobContainerClient = await GetContainerAsync(containerName);

        var blob = blobContainerClient.GetBlobClient(blobName);

        var response = await blob.DeleteIfExistsAsync(cancellationToken: token);

        if (response.Value is false) return new ResponseResult(new BadRequestException("DeleteFailed", $"File ({blobName}) failed to delete"));

        return new();
    }

    public Task<List<KeyValuePair<string, string>>> UploadCollectionCollection(IReadOnlyList<Stream> fileStream)
    {
        throw new NotImplementedException();
    }

    public async Task<ResponseResult<FileUploadedResponse>> UploadFile(Stream fileStream, string fileName, string containerName, CancellationToken cancellationToken, string? folderPath = null, string? contentType = null)
    {
        var blobContainerClient = await GetContainerAsync(containerName);

        var extension = FileHelper.GetFileExtension(fileName);

        var uniqueFileName = $"{Guid.NewGuid()}{extension}";

        BlobClient blobClient = blobContainerClient.GetBlobClient($"{folderPath}/{uniqueFileName}");

        var blobContentInfo = await blobClient.UploadAsync(fileStream, new BlobHttpHeaders
        {
            ContentType = contentType ?? ContentType
        }, cancellationToken: cancellationToken);

        if (blobContentInfo.GetRawResponse().IsError) return new(new BadRequestException("FileUpload", $"File ({fileName}) failed to upload"));

        FileUploadedResponse file = new()
        {
            BlobName = blobClient.Name,
            FileName = fileName,
            UniqueName = uniqueFileName,
            URI = blobClient.Uri.ToString(),
        };

        return new(file);
    }

    private async Task<BlobContainerClient> GetContainerAsync(string containerName)
    {
        var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);

        await containerClient.CreateIfNotExistsAsync(publicAccessType: PublicAccessType.Blob);

        return containerClient;
    }
}
