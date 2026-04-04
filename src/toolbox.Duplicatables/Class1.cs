namespace toolbox.Duplicatables;
/// <summary>
/// Defines an interface for cloning objects. This is a key component of the Prototype design pattern,
/// allowing objects to create copies of themselves without depending on their concrete classes.
/// </summary>
/// <typeparam name="TDuplicatable"></typeparam>
public interface IDuplicatable <TDuplicatable> {
    public TDuplicatable Copy();
}