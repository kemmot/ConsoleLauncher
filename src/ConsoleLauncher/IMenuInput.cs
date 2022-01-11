namespace ConsoleLauncher
{
    using System.Collections.Generic;

    public interface IMenuInput
    {
        List<MenuItem> GetInputItems();
    }
}
