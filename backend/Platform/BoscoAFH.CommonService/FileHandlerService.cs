using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace BoscoAFH.CommonService
{
    public interface IFileHandlerService
    {
        /// <summary>
        /// Saves the file with a unique name in the given path.
        /// </summary>
        /// <param name="file">File to save</param>
        /// <param name="directoryPath">Path to save</param>
        /// <returns>Full path of the file after saved</returns>
        string SaveUniqueFile(IFormFile file, string directoryPath, string? fileName = null);

        string SaveUniqueFile(MemoryStream stream, string directoryPath, string fileName, string fileExtension);

        bool DeleteFile(string directoryPath);

        string SaveFileWithOriginalName(IFormFile file, string directoryPath);

        string ReplaceFile(IFormFile file, string filePath);

        byte[] GetFile(string directoryPath, string fileName);
    }

    public class FileHandlerService(IConfiguration configuration): IFileHandlerService
    {
        /// <summary>
        /// Save the file with a unique name in the given path
        /// </summary>
        /// <param name="file">File to save</param>
        /// <param name="directoryPath">Path to save</param>
        /// <returns>Full path of the file after saved</returns>
        /// <exception cref="ArgumentException"></exception>
        public string SaveUniqueFile(IFormFile file, string directoryPath, string? fileName = null)
        {
            string BasePath = Path.Combine(configuration["AppStrings:GatewayRoot"], directoryPath);

            if (file == null || string.IsNullOrEmpty(file.FileName))
            {
                throw new ArgumentException("File is invalid.");
            }
            if (!Directory.Exists(BasePath))
            {
                Directory.CreateDirectory(BasePath);
            }

            var fileExtension = Path.GetExtension(file.FileName);
            //var uniqueFileName = $"{Guid.NewGuid()}_{DateTime.Now:ddMMyyyy}{fileExtension}"; // Fix date format
            var uniqueFileName = !string.IsNullOrEmpty(fileName) ? $"{fileName}_{DateTime.Now:ddMMyyyy}{fileExtension}" : $"{Guid.NewGuid()}_{DateTime.Now:ddMMyyyy}{fileExtension}";
            var fullPath = Path.Combine(BasePath, uniqueFileName);

            using (var fileStream = new FileStream(fullPath, FileMode.Create))
            {
                file.CopyTo(fileStream);
            }

            return fullPath;
        }

        public string SaveUniqueFile(MemoryStream stream, string directoryPath, string fileName, string fileExtension)
        {
            if (stream == null || stream.Length == 0)
            {
                throw new ArgumentException("Stream is invalid.");
            }

            string basePath = Path.Combine(configuration["AppStrings:GatewayRoot"], directoryPath);
            if (!Directory.Exists(basePath))
            {
                Directory.CreateDirectory(basePath);
            }

            fileExtension = fileExtension.StartsWith('.') ? fileExtension : $".{fileExtension}";
            string uniqueFileName = !string.IsNullOrEmpty(fileName) ? $"{fileName}_{DateTime.Now:ddMMyyyy}{fileExtension}"
                                                                    : $"{Guid.NewGuid()}_{DateTime.Now:ddMMyyyy}{fileExtension}";

            string fullPath = Path.Combine(basePath, uniqueFileName);

            using (var fileStream = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
            {
                stream.WriteTo(fileStream);
            }

            return fullPath;
        }

        public bool DeleteFile(string directoryPath)
        {
            bool result = false;
            string BasePath = Path.Combine(configuration["AppStrings:GatewayRoot"], directoryPath);

            if (directoryPath == null || string.IsNullOrEmpty(directoryPath))
            {
                throw new ArgumentException("File is invalid.");
            }
            if (System.IO.File.Exists(BasePath))
            {
                System.IO.File.Delete(BasePath);
                result = true;
            }
            return result;
        }

        public string SaveFileWithOriginalName(IFormFile file, string directoryPath)
        {
            string basePath = Path.Combine(configuration["AppStrings:GatewayRoot"], directoryPath);

            if (file == null || string.IsNullOrEmpty(file.FileName))
            {
                throw new ArgumentException("File is invalid.");
            }

            if (!Directory.Exists(basePath))
            {
                Directory.CreateDirectory(basePath);
            }

            // Get original filename without extension
            var fileNameWithoutExt = Path.GetFileNameWithoutExtension(file.FileName);
            var fileExtension = Path.GetExtension(file.FileName);

            // Add timestamp to avoid duplicates
            var uniqueFileName = $"{fileNameWithoutExt}_{DateTime.Now:yyyyMMddHHmmss}{fileExtension}";
            var fullPath = Path.Combine(basePath, uniqueFileName);

            using (var fileStream = new FileStream(fullPath, FileMode.Create))
            {
                file.CopyTo(fileStream);
            }

            // Return just the filename (not full path) if needed
            return uniqueFileName;
        }

        public string ReplaceFile(IFormFile file, string filePath)
        {
            string directoryPath = Path.GetDirectoryName(filePath) ?? string.Empty;
            string basePath = Path.Combine(configuration["AppStrings:GatewayRoot"], directoryPath);
            string fullPath = Path.Combine(basePath, Path.GetFileName(filePath));
            if (file == null || string.IsNullOrEmpty(file.FileName))
            {
                throw new ArgumentException("File is invalid.");
            }
            if (!Directory.Exists(basePath))
            {
                Directory.CreateDirectory(basePath);
            }
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }
            using (var fileStream = new FileStream(fullPath, FileMode.Create))
            {
                file.CopyTo(fileStream);
            }
            return fullPath;
        }

        public byte[] GetFile(string directoryPath, string fileName)
        {
            string basePath = Path.Combine(configuration["AppStrings:GatewayRoot"], directoryPath);
            string fullPath = Path.Combine(basePath, fileName);

            if (System.IO.File.Exists(fullPath))
            {
                return System.IO.File.ReadAllBytes(fullPath);
            }
            return null;
        }
    }
}
