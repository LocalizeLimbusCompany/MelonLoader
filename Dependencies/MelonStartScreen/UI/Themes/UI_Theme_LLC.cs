namespace MelonLoader.MelonStartScreen.UI.Themes
{
    internal class UI_Theme_LLC : UI_Theme
    {
        internal UI_Theme_LLC() => Defaults();

        internal override byte[] GetLoadingImage()
            => StartScreenResources.LLC_Zero_Logo;

        internal override byte[] GetLogoImage()
            => StartScreenResources.LLC_Zero_Logo;
    }
}
