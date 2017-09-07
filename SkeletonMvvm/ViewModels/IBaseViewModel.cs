namespace SkeletonMvvm
{
    public interface IBaseViewModel
    {
    }

    public interface IBaseViewModel<in TNavData> : IBaseViewModel
    {
        void Init(TNavData navData);
    }
}