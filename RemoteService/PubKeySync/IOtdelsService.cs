using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace RemoteService.PubKeySync
{
    [ServiceContract]
    public interface IOtdelsService
    {
        [OperationContract]
        [WebGet(UriTemplate = "ShowPosts",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json )]
        List<PostInformations> ShowPosts();

        [OperationContract]
        [WebGet(UriTemplate = "ShowUsers",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json)]
        List<UserInformations> ShowSignableUsers();
        
        [OperationContract]
        [WebInvoke(Method = "Post", 
            RequestFormat = WebMessageFormat.Xml, 
            ResponseFormat = WebMessageFormat.Xml,
            UriTemplate = "RegKey")]
        bool NewKeyRegistration(NewPublicKeyInfo newPublicKey);


    }

    [DataContract]
    public class PostInformations
    {
        [DataMember]
        public string PostName { get; set; }
    }

    [DataContract]
    public class UserInformations
    {
        [DataMember]
        public string FullName { get; set; }

        [DataMember]
        public Guid UserId { get; set; }
    }

    [DataContract]
    public class NewPublicKeyInfo
    {

        [DataMember]
        public string UserIdentity { get; set; }

        [DataMember]
        public long KeyFingerPrint { get; set; }

        [DataMember]
        public DateTime KeyGenerationTime { get; set; }

        [DataMember]
        public DateTime KeyExpirationTime { get; set; }

        [DataMember]
        public byte[] KeyBody { get; set; }

        [DataMember]
        public Guid SelectedUserID { get; set; }

    }
    


}
