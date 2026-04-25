namespace toolbox.DiscriminatedUnions; 

public class DiscriminatedUnion <TValue, TError> : IDiscriminatedable <TValue, TError> {
    
    private TError? error;
    private TValue? value;
    private bool isValue;
    
    // -- constructors
    protected DiscriminatedUnion (bool isValue)
        => this.isValue = isValue;

    public DiscriminatedUnion (TValue value): this(true)
        => this.value = value;

    public DiscriminatedUnion (TError error) : this(false)
        => this.error = error;
    
    // -- implemented interfaces
    public IDiscriminatedable <TValue, TError> Then (Action <TValue> action) {
        // value flag is primary indicator for state of discriminated union
        if (this.isValue && this.value is not null)
            action(this.value); 
        return this;
    }

    public IDiscriminatedable <TValue, TError> Then (Action <TError> action) {
        if (!this.isValue && this.error is not null)
            action(this.error);
        return this;
    }
    
    // -- implicit operators
    /// <summary>
    /// Simplifies the conversion of value into Discriminated Union. The state of resulting
    /// Discriminated Union is based on type of value to convert.
    /// </summary>
    /// <param name="value">Defines state of Discriminated Union.</param>
    /// <returns>A Discriminated Union, state of union is based on value.</returns>
    public static implicit operator DiscriminatedUnion <TValue, TError> (TValue value)
        => new(value);

    /// <summary>
    /// Simplifies the conversion of value into Discriminated Union. The state of resulting
    /// Discriminated Union is based on type of value to convert.
    /// </summary>
    /// <param name="value">Defines state of Discriminated Union.</param>
    /// <returns>A Discriminated Union, state of union is based on value.</returns>
    public static implicit operator DiscriminatedUnion <TValue, TError> (TError error)
        => new(error);
}