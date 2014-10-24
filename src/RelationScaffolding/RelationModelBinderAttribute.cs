using System.Web.Mvc;

namespace RelationScaffolding
{
    public class RelationModelBinderAttribute : CustomModelBinderAttribute
    {
        public override IModelBinder GetBinder()
        {
            return new RelationModelBinder();
        }
    }
}
