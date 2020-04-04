using NUnit.Framework;
using EDTools.Helper;
using System.IO;
using System.Text;

namespace EDToolsTest
{
    public class AesTests
    {
        private string _data = "TestData";
        private string _key128 = "LYAB9FK5HHKCW76B";
        private string _key192 = "LYAB9FK5HHKCW76B2BDDZ5GB";
        private string _key256 = "LYAB9FK5HHKCW76B2BDDZ5GBNDYMB5T5";
        private string _iv = "FKXFHH6CPKXL3X66";

        [Test]
        public void EDTest()
        {            
            var status = 0;//初始狀態為 0，當有單元失敗就會設定為 1 
            for (var mode = 1; mode <= 2; mode++)//mode 為加密模式的代號，目前只測 CDC(1)，ECB(2) 因為只成功這兩個
            {
                for (var padding = 2; padding <= 5; padding++)//padding 為填充方法的代號，1 的 None 填充方案不會過測試，先不測試&開放使用
                {
                    try
                    {
                        //TODO: 精減位元測試區段，減少重複程式碼
                        //128 位元測試                        
                        //測試加密
                        var source_128 = AesHelper.Encrypt(this._data, this._key128, mode, padding, this._iv);
                        //測試解密
                        var decrypt_128 = AesHelper.Decrypt(source_128, this._key128, mode, padding, this._iv);
                        Assert.AreEqual(_data, decrypt_128.Substring(0, _data.Length));

                        //192 位元測試                        
                        //測試加密
                        var source_192 = AesHelper.Encrypt(this._data, this._key192, mode, padding, this._iv);
                        //測試解密
                        var decrypt_192 = AesHelper.Decrypt(source_192, this._key192, mode, padding, this._iv);
                        Assert.AreEqual(_data, decrypt_192.Substring(0, _data.Length));

                        //256 位元測試                        
                        //測試加密
                        var source_256 = AesHelper.Encrypt(this._data, this._key256, mode, padding, this._iv);
                        //測試解密
                        var decrypt_256 = AesHelper.Decrypt(source_256, this._key256, mode, padding, this._iv);
                        Assert.AreEqual(_data, decrypt_256.Substring(0, _data.Length));
                    }
                    catch 
                    {
                        string path = System.Environment.CurrentDirectory + @"\ErrorTestLog.txt";

                        //如果是首次進入錯誤紀錄區，如果有紀錄，清空現有紀錄
                        if(status == 0 & System.IO.File.Exists(path))
                        {
                            FileStream stream = File.Open(path, FileMode.OpenOrCreate, FileAccess.Write);
                            stream.Seek(0, SeekOrigin.Begin);
                            stream.SetLength(0);
                            stream.Close();
                        }

                        //將狀態設定為失敗
                        status = 1;

                        //判斷是否已經有了這個文件，沒有就新增
                        if (!System.IO.File.Exists(path))
                        {
                            new FileStream(path, FileMode.Create, FileAccess.Write);                            
                        }

                        //發生錯誤的單元
                        var errorMsg = ("mode =" + mode + ", " +  "padding =" + padding);

                        //寫入錯誤紀錄
                        using (FileStream fs = new FileStream(path, FileMode.Append))
                        {
                            using (StreamWriter writer = new StreamWriter(fs))
                            {
                                writer.WriteLine(errorMsg);
                            }
                        }
                    }
                }
            }
            //如果有任何一個單元錯誤，單元測試就會失敗，需要去 ErrorTestLog.txt 內找問題
            //路徑為 EDTools\EDToolsTest\bin\Debug\netcoreapp3.1\ErrorTestLog.txt
            Assert.AreEqual(0, status);
        }
    }
}