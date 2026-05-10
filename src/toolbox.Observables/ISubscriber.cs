namespace toolbox.Observables; 

public interface ISubscriber {}
/// <summary>
/// The interface represents the subscriber component of the Observer Design Pattern. The Subscriber
/// interface declares the notification interface.
/// <see href="https://refactoring.guru/design-patterns/observer"/>
/// </summary>
/// <typeparam name="TContext">Datatype of details, that will be forwarded from the publisher to the
/// subscriber.</typeparam>
public interface ISubscriber <TContext>: ISubscriber {

    /// <summary>
    /// Receives details.x
    /// </summary>
    /// <param name="context"></param>
    public void Notify (TContext context);
}