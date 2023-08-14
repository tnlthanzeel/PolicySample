using Facets.Core.Common.Dtos;
using Facets.SharedKernal.Responses;

namespace Facets.Core.Common.Interfaces;

public interface IFileRespository
{
    Task<ResponseResult<FileUploadedResponse>> UploadFile(Stream fileStream, string documentName, string containerName, CancellationToken cancellationToken, string? folderPath = null, string? contentType = null);
    //Task<List<KeyValuePair<string, string>>> UploadCollectionCollection(IReadOnlyList<Stream> fileStream);

    //Task<IEnumerable<CloudBlockBlob>> ListDocumentBlobsAsync(string prefix = null, bool includeSnapshots = false);

    //Task DownloadDocument(CloudBlockBlob cloudBlockBlob, Stream targetStream);

    Task<ResponseResult> DeleteFile(string containerName, string blobName, CancellationToken cancellationToken);

    //string GetBlobUriWithSasToken(CloudBlockBlob cloudBlockBlob);
}
