namespace toolbox.Observables; 

public class PublisherException: Exception {

    public readonly List <NotifySubscriberException> OccuredExceptions;
    public bool HasErrors { get; private set; }

    // -- constructors
    public PublisherException() : base("Not all subscribers were notified successfully!") {
        this.HasErrors = false;
        this.OccuredExceptions = new List <NotifySubscriberException>();
    }

    public PublisherException (NotifySubscriberException exception) : this() {
        this.OccuredExceptions.Add(exception);
        this.HasErrors = true;
    } 
    
    // -- methods
    public void Add (ISubscriber subscriber, Exception exception) {
        this.OccuredExceptions.Add(new NotifySubscriberException(subscriber, exception));
        this.HasErrors = true;
    }
}