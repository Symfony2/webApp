
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web;
using Bundles;
using OpenPGPzzz;

namespace webf.Models.EMsgModel
{
    public class EMsgSender
    {
        [Display(Name = "Подписанный документ для отправки")]
        [File(AllowedFileExtensions = new string[] { ".ebf" }, MaxContentLength = 1024 * 1024 * 10, ErrorMessage = "Invalid File")]
        public HttpPostedFileBase EMsgTmpFile { get; set; }

        public bool proccessIncomingMsg(ref string errorReason)
        {
            if (EMsgTmpFile == null)
            {
                errorReason = "Отсутсвует файл";
                return false;
            }

            string password = "MyMadPassword";  //////////////***********Пароль***********////////////////////
            byte[] keyFile = new byte[] { };    //////////////***********Файл***********////////////////////

            BinaryFormatter bf = new BinaryFormatter();
            BundleFile recivedFile = (BundleFile)bf.Deserialize(EMsgTmpFile.InputStream);

            PgpEncryptionKeys _keys = new PgpEncryptionKeys("pub.gpg", password, keyFile);
            using (Stream strm = new MemoryStream(recivedFile.EncryptedSignedFile))
            {
                PgpDecrypt decryptor = new PgpDecrypt(_keys);
                decryptor.VerifySignature(strm, null);
            }


            return false;
        }

    }
}