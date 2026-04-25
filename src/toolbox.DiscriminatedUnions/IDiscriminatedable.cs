namespace toolbox.DiscriminatedUnions; 

/// <summary>
/// The interface represents a discriminated union of two possible types (TOne or TTwo).
/// It allows consumers to safely execute logic depending on which type is currently held.
/// This follows the discriminated union pattern, where a value can be one of several predefined types, and behavior is
/// selected accordingly.
/// </summary>
/// <typeparam name="TOne"></typeparam>
/// <typeparam name="TTwo"></typeparam>
public interface IDiscriminatedable <TOne, TTwo> {
    
    /// <summary>
    /// Executes the provided action if the underlying value is of type TOne.
    /// </summary>
    /// <param name="action">The operation to execute when the stored value is of type TOne</param>
    /// <returns></returns>
    public IDiscriminatedable <TOne, TTwo> Then (Action <TOne> action);
    /// <summary>
    /// Executes the provided action if the underlying value is of type TTwo.
    /// </summary>
    /// <param name="action">The operation to execute when the stored value is of type TTwo</param>
    /// <returns></returns>
    public IDiscriminatedable <TOne, TTwo> Then (Action <TTwo> action);
}

/// <summary>
/// The interface represents a discriminated union of three possible types (TOne, TTwo and TThree).
/// It allows consumers to safely execute logic depending on which type is currently held.
/// This follows the discriminated union pattern, where a value can be one of several predefined types, and behavior is
/// selected accordingly.
/// </summary>
/// <typeparam name="TOne"></typeparam>
/// <typeparam name="TTwo"></typeparam>
/// <typeparam name="TThree"></typeparam>
public interface IDiscriminatedable <TOne, TTwo, TThree> {

    /// <summary>
    /// Executes the provided action if the underlying value is of type TOne.
    /// </summary>
    /// <param name="action">The operation to execute when the stored value is of type TOne</param>
    /// <returns></returns>
    public IDiscriminatedable <TOne, TTwo, TThree> Then (Action <TOne> action);
    /// <summary>
    /// Executes the provided action if the underlying value is of type TTwo.
    /// </summary>
    /// <param name="action">The operation to execute when the stored value is of type TTwo</param>
    /// <returns></returns>
    public IDiscriminatedable <TOne, TTwo, TThree> Then (Action <TTwo> action);
    /// <summary>
    /// Executes the provided action if the underlying value is of type TThree.
    /// </summary>
    /// <param name="action">The operation to execute when the stored value is of type TThree</param>
    /// <returns></returns>
    public IDiscriminatedable <TOne, TTwo, TThree> Then (Action <TThree> action);
}