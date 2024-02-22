using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static FilesValidator.FilesComparator;

namespace FilesValidator
{
    internal class VerificationFile
    {
        internal enum FilePathMode { single, multi };
        internal enum EncryptingMode { md5, sha256 };

        internal DateTime createdTime;
        internal FilePathMode fileMode;
        internal string filePath;
        internal int filesCount;
        internal EncryptingMode encryptingMode;
        internal Dictionary<string, string> hashCode;

        private FileStream? fileStream;
        private MD5 md5;
        private SHA256 sha256;

        internal VerificationFile(string verificationFilePath)
        {
            fileStream = new FileStream(verificationFilePath, FileMode.Open, FileAccess.Read);
            StreamReader streamReader = new StreamReader(fileStream, encoding: Encoding.UTF8);
            string? line;
            filePath = "";
            hashCode = new Dictionary<string, string>();
            try
            {
                if((line = streamReader.ReadLine()) != null)
                {
                    line = line.Substring(15, line.Length - 15);
                    createdTime = DateTime.Parse(line);
                }
                if((line = streamReader.ReadLine()) != null)
                {
                    line = line.Substring(12, line.Length - 12);
                    fileMode = line.Equals("single") ? FilePathMode.single : FilePathMode.multi;
                }
                if((line = streamReader.ReadLine()) != null)
                {
                    line = line.Substring(7, line.Length - 7);
                    filePath = line;
                }
                if((line = streamReader.ReadLine()) != null)
                {
                    line = line.Substring(14, line.Length - 14);
                    filesCount = int.Parse(line);
                }
                if((line = streamReader.ReadLine()) != null)
                {
                    line = line.Substring(17, line.Length - 17);
                    encryptingMode = line.Equals("md5") ? EncryptingMode.md5 : EncryptingMode.sha256;
                }
                streamReader.ReadLine();
                int index;
                while((line = streamReader.ReadLine()) != null)
                {
                    index = line.IndexOf('=');
                    hashCode.Add(line.Substring(0, index - 1), line.Substring(index + 2, line.Length - index - 2));
                }
                streamReader.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("无法正确读取校验文件！\n"+e.ToString(), "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                md5 = MD5.Create();
                sha256 = SHA256.Create();
                return;
            }

            md5 = MD5.Create();
            sha256 = SHA256.Create();
        }
        internal VerificationFile(FilePathMode fileMode, string filePath, EncryptingMode encryptingMode)
        {
            createdTime = DateTime.Now;
            this.fileMode = fileMode;
            if(filePath[filePath.Length - 1] == '\\')
            {
                filePath = filePath.Substring(0, filePath.Length - 1);
            }
            this.filePath = filePath;
            filesCount = 0;
            this.encryptingMode = encryptingMode;
            hashCode = new Dictionary<string, string>();

            md5 = MD5.Create();
            sha256 = SHA256.Create();
        }
        internal void getFilesCount(string currentPath)
        {
            filesCount += Directory.GetFiles(currentPath).Length;
            //filesCount += Directory.GetDirectories(currentPath).Length;
            foreach(string subdir in Directory.GetDirectories(currentPath))
            {
                getFilesCount(subdir);
            }
        }
        private string CreateHashCode(FileStream fileStream)
        {
            byte[] HashCodeByte;
            if(encryptingMode == EncryptingMode.md5)
            {
                HashCodeByte = md5.ComputeHash(fileStream);
            }
            else
            {
                HashCodeByte = sha256.ComputeHash(fileStream);
            }
            string HashCodeString = string.Empty;
            foreach(byte byteUnit in HashCodeByte)
            {
                HashCodeString += byteUnit.ToString("x");
            }
            return HashCodeString;
        }
        internal void LoopThroughPath(string currentPath, Func<bool> ifCancelled, Action<string?> UIUpgrade)
        {
            if(fileMode == FilePathMode.multi)
            {
                foreach(string subdir in Directory.GetDirectories(currentPath))
                {
                    LoopThroughPath(subdir, ifCancelled, UIUpgrade);
                }
                foreach(string subFile in Directory.GetFiles(currentPath))
                {
                    if(ifCancelled())
                    {
                        return;
                    }
                    UIUpgrade(subFile);
                    fileStream = new FileStream(subFile, FileMode.Open);
                    hashCode.Add(subFile, CreateHashCode(fileStream));
                    fileStream.Close();
                }
            }
            else
            {
                fileStream = new FileStream(currentPath, FileMode.Open);
                hashCode.Add(currentPath, CreateHashCode(fileStream));
                fileStream.Close();
                UIUpgrade(currentPath);
            }
        }
        internal string createFileName()
        {
            int dirIndex = filePath.LastIndexOf('\\');
            string fileName = filePath.Substring(0, dirIndex + 1);
            if(fileMode == FilePathMode.single)
            {
                fileName += filePath.Substring(dirIndex + 1, filePath.LastIndexOf('.') - dirIndex - 1);
            }
            else
            {
                fileName += filePath.Substring(dirIndex + 1, filePath.Length - dirIndex - 1);
            }
            fileName += "_" + createdTime.ToString("yyyy-MM-dd_HHmmss") + ".vf";
            return fileName;
        }
        internal void WriteToFile(Func<bool> ifCancelled, Action<string?> UIUpgrade)
        {
            fileStream = new FileStream(createFileName(), FileMode.CreateNew, FileAccess.Write);
            StreamWriter streamWriter = new StreamWriter(fileStream, encoding: Encoding.UTF8);
            streamWriter.WriteLine("Created time = " + createdTime.ToString("G"));
            streamWriter.WriteLine("File mode = " + fileMode.ToString());
            streamWriter.WriteLine("Path = " + filePath);
            streamWriter.WriteLine("Files count = " + filesCount.ToString());
            streamWriter.WriteLine("Encrypted mode = " + encryptingMode.ToString());
            streamWriter.WriteLine();
            foreach(KeyValuePair<string, string> keyValuePair in hashCode)
            {
                streamWriter.WriteLine(keyValuePair.Key + " = " + keyValuePair.Value);
                if(ifCancelled())
                {
                    return;
                }
                UIUpgrade(null);
            }
            streamWriter.Close();
        }
    }
}
