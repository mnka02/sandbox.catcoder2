namespace toolbox.Observables; 

public interface ISubscribable <TContext> {
    
    /// <summary>
    /// Adds the subscriber to the list. 
    /// </summary>
    /// <param name="subscriber">Joins the list of subscribers.</param>
    public void Subscribe (ISubscriber <TContext> subscriber);
    /// <summary>
    /// Remove a subscriber from the list.
    /// </summary>
    /// <param name="subscriber">Leaves the list of subscribers.</param>
    public void Unsubscribe (ISubscriber <TContext> subscriber);
}