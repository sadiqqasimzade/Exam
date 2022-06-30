using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EXAM.Utilities.Extentions
{
    public static class FileExtentions
    {
        public static async Task<string> SaveFile(this IFormFile file,string path,int namemaxlen)
        {
            string filename = Guid.NewGuid().ToString() + file.FileName;
            if (filename.Length > namemaxlen)
                filename = filename.Substring(filename.Length - namemaxlen, namemaxlen);

            using (FileStream fs = new FileStream(Path.Combine(path, filename), FileMode.Create))
                await file.CopyToAsync(fs);

            return filename;
        }
        public static void DeleteFile(this string filename,string path)
        {
            if (File.Exists(Path.Combine(path, filename)))
                File.Delete(Path.Combine(path, filename));
        }
        
    }
}
