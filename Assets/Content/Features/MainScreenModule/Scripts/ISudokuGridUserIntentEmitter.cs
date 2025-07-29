namespace Content.Features.MainScreenModule.Scripts
{
    public interface ISudokuGridUserIntentEmitter
    {
        void RequestEdit(int gridId);
        void RequestCreation();
    }
}