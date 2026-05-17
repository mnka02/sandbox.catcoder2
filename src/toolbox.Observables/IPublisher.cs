namespace toolbox.Observables; 

/// <summary>
/// The interface represents the publisher component of the Observer Design Pattern. The Publisher
/// issues events of interest to other objects. These events occur when the publisher changes its
/// state or executes some behaviors. Publishers contain a subscription infrastructure that lets
/// new subscribers join and current subscribers leave the list.
/// <see href="https://refactoring.guru/design-patterns/observer"/>
/// </summary>
/// <typeparam name="TContext">Datatype of value, that will be forwarded to each subscriber</typeparam>
public interface IPublisher <TContext> : ISubscribable <TContext> {
    
    /// <summary>
    /// Issues details to each subscriber.
    /// </summary>
    /// <param name="context">Is forwarded to each subscriber.</param>
    public void NotifySubscribers (TContext context);
}