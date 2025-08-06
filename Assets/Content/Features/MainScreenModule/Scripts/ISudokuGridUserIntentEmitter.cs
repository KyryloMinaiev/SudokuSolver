namespace Content.Features.MainScreenModule.Scripts
{
    public interface ISudokuGridUserIntentEmitter
    {
        void RequestEdit(long gridId);
        void RequestCreation();
    }
}