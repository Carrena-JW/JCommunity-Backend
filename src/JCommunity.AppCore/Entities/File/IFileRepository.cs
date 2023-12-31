﻿namespace JCommunity.AppCore.Entities.File;

public interface IFileRepository 
{
    Task<string> SaveFileAsync(IFormFile file,bool containThumnail= false, CancellationToken token = new());
    Task<(string FileType, Stream FileStream)> GetFileAsync(string fileName, CancellationToken token=new());
}
