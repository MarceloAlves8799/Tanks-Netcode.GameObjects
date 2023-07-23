public interface ICollectable
{
    bool AlreadyCollect { get;  set; }

    void OnCollect();
}
