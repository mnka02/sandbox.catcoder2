namespace toolbox.Observables; 

public class NotifySubscriberException: Exception {

    public readonly ISubscriber Subscriber;
    public readonly Exception OccuredException;

    // -- constructor
    public NotifySubscriberException (ISubscriber subscriber, Exception exception) : base(exception.Message) {
        this.Subscriber = subscriber;
        this.OccuredException = exception;
    }
}