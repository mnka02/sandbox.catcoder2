namespace toolbox.Pipeline; 

// TODO: #7
/// <summary>
/// Implementation of the Strategy Design Pattern. 
/// </summary>
/// <typeparam name="TContext"></typeparam>
public interface IBlockStrategy <TContext> {
    
    public TContext Execute (TContext context);
}