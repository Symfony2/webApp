

using webf.Models.EntityModel;

namespace webf.Models
{
    public class Forbear
    {
        public EntityModel.FlexDBEntities db;
        
        private static Forbear _forbear = new Forbear();

        public static Forbear GetInstance()
        {
            return _forbear;
        }

        private Forbear()
        {
            db= new FlexDBEntities();
        }
    }
}