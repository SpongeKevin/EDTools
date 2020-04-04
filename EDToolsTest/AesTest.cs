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
            var status = 0;//��l���A�� 0�A���椸���ѴN�|�]�w�� 1 
            for (var mode = 1; mode <= 2; mode++)//mode ���[�K�Ҧ����N���A�ثe�u�� CDC(1)�AECB(2) �]���u���\�o���
            {
                for (var padding = 2; padding <= 5; padding++)//padding ����R��k���N���A1 �� None ��R��פ��|�L���աA��������&�}��ϥ�
                {
                    try
                    {
                        //TODO: ���줸���հϬq�A��֭��Ƶ{���X
                        //128 �줸����                        
                        //���ե[�K
                        var source_128 = AesHelper.Encrypt(this._data, this._key128, mode, padding, this._iv);
                        //���ոѱK
                        var decrypt_128 = AesHelper.Decrypt(source_128, this._key128, mode, padding, this._iv);
                        Assert.AreEqual(_data, decrypt_128.Substring(0, _data.Length));

                        //192 �줸����                        
                        //���ե[�K
                        var source_192 = AesHelper.Encrypt(this._data, this._key192, mode, padding, this._iv);
                        //���ոѱK
                        var decrypt_192 = AesHelper.Decrypt(source_192, this._key192, mode, padding, this._iv);
                        Assert.AreEqual(_data, decrypt_192.Substring(0, _data.Length));

                        //256 �줸����                        
                        //���ե[�K
                        var source_256 = AesHelper.Encrypt(this._data, this._key256, mode, padding, this._iv);
                        //���ոѱK
                        var decrypt_256 = AesHelper.Decrypt(source_256, this._key256, mode, padding, this._iv);
                        Assert.AreEqual(_data, decrypt_256.Substring(0, _data.Length));
                    }
                    catch 
                    {
                        string path = System.Environment.CurrentDirectory + @"\ErrorTestLog.txt";

                        //�p�G�O�����i�J���~�����ϡA�p�G�������A�M�Ų{������
                        if(status == 0 & System.IO.File.Exists(path))
                        {
                            FileStream stream = File.Open(path, FileMode.OpenOrCreate, FileAccess.Write);
                            stream.Seek(0, SeekOrigin.Begin);
                            stream.SetLength(0);
                            stream.Close();
                        }

                        //�N���A�]�w������
                        status = 1;

                        //�P�_�O�_�w�g���F�o�Ӥ��A�S���N�s�W
                        if (!System.IO.File.Exists(path))
                        {
                            new FileStream(path, FileMode.Create, FileAccess.Write);                            
                        }

                        //�o�Ϳ��~���椸
                        var errorMsg = ("mode =" + mode + ", " +  "padding =" + padding);

                        //�g�J���~����
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
            //�p�G������@�ӳ椸���~�A�椸���մN�|���ѡA�ݭn�h ErrorTestLog.txt ������D
            //���|�� EDTools\EDToolsTest\bin\Debug\netcoreapp3.1\ErrorTestLog.txt
            Assert.AreEqual(0, status);
        }
    }
}