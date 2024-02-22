using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FilesValidator
{
    internal class FilesComparator
    {
        internal enum CompareResult { equal, lost, changed, moved, movedNewPosition, newAdded };

        internal VerificationFile? earlierFile;
        internal VerificationFile? laterFile;

        private string path1;
        private string path2;

        public int equalFilesCount;
        public int lostFilesCount;
        public int changedFilesCount;
        public int movedFilesCount;
        public int newFilesCount;

        private Dictionary<string, string> movedPosition;

        internal FilesComparator(string filePath1,  string filePath2)
        {
            path1 = filePath1;
            path2 = filePath2;

            equalFilesCount = 0;
            lostFilesCount = 0;
            changedFilesCount = 0;
            movedFilesCount = 0;
            newFilesCount = 0;

            movedPosition = new Dictionary<string, string>();
        }
        internal void ReadVerificationFiles()
        {
            VerificationFile vf1 = new VerificationFile(path1);
            VerificationFile vf2 = new VerificationFile(path2);
            if(DateTime.Compare(vf1.createdTime, vf2.createdTime) <= 0)
            {
                earlierFile = vf1;
                laterFile = vf2;
            }
            else
            {
                earlierFile = vf2;
                laterFile = vf1;
            }
        }
        internal void CompareTwoFiles(Func<bool> ifCancelled, Action<CompareResult, string?> UIUpgrade)
        {
            if( earlierFile == null || laterFile == null)
            {
                return;
            }
            CompareResult compareResult;
            foreach(KeyValuePair<string,string> keyValuePair in earlierFile.hashCode)
            {
                if(ifCancelled())
                {
                    return;
                }
                if(laterFile.hashCode.ContainsKey(keyValuePair.Key))
                {
                    if(laterFile.hashCode.Contains(keyValuePair))
                    {
                        compareResult = CompareResult.equal;
                        laterFile.hashCode.Remove(keyValuePair.Key);
                        equalFilesCount++;
                    }
                    else
                    {
                        compareResult = CompareResult.changed;
                        laterFile.hashCode.Remove(keyValuePair.Key);
                        changedFilesCount++;
                    }
                }
                else if(laterFile.hashCode.ContainsValue(keyValuePair.Value))
                {
                    compareResult = CompareResult.moved;
                    movedFilesCount++;
                    movedPosition.Add(keyValuePair.Value, keyValuePair.Key);
                }
                else
                {
                    compareResult = CompareResult.lost;
                    lostFilesCount++;
                }
                UIUpgrade(compareResult, keyValuePair.Key);
            }
            bool flag;
            foreach(KeyValuePair<string,string> keyValuePair in laterFile.hashCode)
            {
                flag = false;
                foreach(KeyValuePair<string,string> valuePair in movedPosition)
                {
                    if(ifCancelled())
                    {
                        return;
                    }
                    if(keyValuePair.Value == valuePair.Key)
                    {
                        UIUpgrade(CompareResult.movedNewPosition, valuePair.Value+" => "+keyValuePair.Key);
                        flag = true;
                    }
                }
                if(!flag)
                {
                    newFilesCount++;
                    UIUpgrade(CompareResult.newAdded, keyValuePair.Key);
                }
            }
        }
    }
}
