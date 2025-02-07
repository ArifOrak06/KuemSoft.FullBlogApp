using KuemSoft.FullBlogApp.Core.DTOs.ImgDTOs;
using KuemSoft.FullBlogApp.SharedLibrary.Enums;
using KuemSoft.FullBlogApp.SharedLibrary.ResponseResultPattern;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace KuemSoft.FullBlogApp.Service.Helpers
{
    public class ImgHelper : IImgHelper
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        private readonly string wwwwroot;
        // Klasör fieldları
        private const string imgFolder = "images";
        private const string articleImagesFolder = "article-images";
        private const string appUserImagesFolder = "appUser-images";
        public ImgHelper(IWebHostEnvironment webHostEnvironment)
        {

            _webHostEnvironment = webHostEnvironment;
            wwwwroot = _webHostEnvironment.WebRootPath; // wwwroot dizini
        }

        public async Task<CustomResponseDto<NoContentDto>> RemoveImageAsync(string imageName)
        {
            // Öncelikle parametre olarak gelen dosya ismi kayıtlı mı değil mi onun kontrolünü yapalım
            var deleteToFile = Path.Combine($"{wwwwroot}/{imgFolder}/{imageName}");
            if (File.Exists(deleteToFile))
            {
                File.Delete(deleteToFile);
                return CustomResponseDto<NoContentDto>.Success(ResponseType.Success,$"{imageName} isimli resim başarılı bir şekilde dizinden silinmiştir.");
            }
            return await Task.FromResult(CustomResponseDto<NoContentDto>.Fail(ResponseType.NotFound, $"{imageName} isimli resim dizinde bulunamamıştır."));
            
        }

        public async Task<CustomResponseDto<ImgUploadDto>> UploadImageAsync(string name, IFormFile formFile, ImageType imageType, string folderName = null)
        {
            folderName ??= imageType == ImageType.User ? appUserImagesFolder : articleImagesFolder;

            // Oluşturulan Dizin daha önce oluşturulmuş mu ? oluşturulmamışsa oluşturalım.

            if (!Directory.Exists($"{wwwwroot}/{imgFolder}/{folderName}"))
            {
                Directory.CreateDirectory($"{wwwwroot}/{imgFolder}/{folderName}");
            }
            //  // Daha sonra yeni gelen resimin isimlendirme işlemlerine başlayalım.
            string oldFileName = Path.GetFileNameWithoutExtension(formFile.FileName);
            // parametre olarak gelen resmin uzantısını alalım.
            var fileExtension = Path.GetExtension(formFile.FileName);
            var replaceName = ReplaceInvalidChars(name);
            // Yeni resim adını benzersiz bir isim yapalım.
            DateTime dateTime = DateTime.Now;
            string newFileName = $"{replaceName}.{dateTime.Millisecond}{fileExtension}";

            //yeni resmin kaydedileceği dosya yolunu oluşturalım
            var path = Path.Combine($"{wwwwroot}/{imgFolder}/{folderName}", newFileName);
            // artık yeni dosya yoluna resmi kaydedelim.
            await using var stream = new FileStream(path,FileMode.Create,FileAccess.Write,FileShare.None,1024 * 1024,useAsync:false);
            await formFile.CopyToAsync(stream);
            await stream.FlushAsync();
            return CustomResponseDto<ImgUploadDto>.Success(ResponseType.Success, new ImgUploadDto
            {
                FullName = $"{folderName}/{newFileName}"

            }, $"{newFileName} isimli resim başarılı bir şekilde upload edilmiştir.");
        }
        private string ReplaceInvalidChars(string imageName)
        {

            return imageName.Replace("İ", "I")
                .Replace("ı", "i")
                .Replace("Ğ", "G")
                .Replace("ğ", "g")
                .Replace("Ü", "U")
                .Replace("ü", "u")
                .Replace("ş", "s")
                .Replace("Ş", "S")
                .Replace("Ö", "O")
                .Replace("ö", "o")
                .Replace("Ç", "C")
                .Replace("ç", "c")
                .Replace("é", "")
                .Replace("!", "")
                .Replace("'", "")
                .Replace("^", "")
                .Replace("+", "")
                .Replace("%", "")
                .Replace("/", "")
                .Replace("(", "")
                .Replace(")", "")
                .Replace("=", "")
                .Replace("?", "")
                .Replace("_", "")
                .Replace("*", "")
                .Replace("æ", "")
                .Replace("ß", "")
                .Replace("@", "")
                .Replace("€", "")
                .Replace("<", "")
                .Replace(">", "")
                .Replace("#", "")
                .Replace("$", "")
                .Replace("½", "")
                .Replace("{", "")
                .Replace("[", "")
                .Replace("]", "")
                .Replace("}", "")
                .Replace(@"\", "")
                .Replace("|", "")
                .Replace("~", "")
                .Replace("¨", "")
                .Replace(",", "")
                .Replace(";", "")
                .Replace("`", "")
                .Replace(".", "")
                .Replace(":", "")
                .Replace(" ", "");
        }
   

    }
}
