using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Objects;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using RemoteService.PubKeySync;
using webf.Models.EntityModel;

namespace webf.API
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "OtdelsService" in code, svc and config file together.
    public class OtdelsService : IOtdelsService
    {
        private FlexDBEntities db = new FlexDBEntities();
        public List<PostInformations> ShowPosts()
        {
            List<PostInformations> prof = db.Posts.Select(val => 
                new PostInformations { PostName = val.PostName }).ToList();
            
            return prof;
        }

        public List<UserInformations> ShowSignableUsers()
        {
            ObjectResult<PersonsWhithSignRights_Result2> signablePersons = db.PersonsWhithSignRights();

            return signablePersons.Select(val=>new UserInformations{FullName = val.FullName, UserId = val.UserID}).ToList();
        }

        public bool NewKeyRegistration(NewPublicKeyInfo newPublicKey)
        {
            Guid _genGuid = Guid.NewGuid();
            try
            {
                UserProfile _selectedUser =
                    db.UserProfile.FirstOrDefault(val => val.UserProfileID == newPublicKey.SelectedUserID);
                _selectedUser.KeyID = _genGuid;
                db.ObjectStateManager.ChangeObjectState(_selectedUser, EntityState.Modified);

                KeyTable _table = new KeyTable();
                _table.GenerationTime = newPublicKey.KeyGenerationTime;
                _table.ExpirationTime = newPublicKey.KeyExpirationTime;
                _table.FingerPrint = newPublicKey.KeyFingerPrint;
                _table.HostIdentity = newPublicKey.UserIdentity;
                _table.TableID = _genGuid;
                
                db.KeyTable.AddObject(_table);
                db.SaveChanges();

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
            
        }
    }
}
