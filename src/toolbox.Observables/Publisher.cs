using System.Collections.Immutable;

namespace toolbox.Observables; 

public class Publisher <TContext> : IPublisher <TContext> {

    // + Uniqueness matters - automatically prevents duplicates
    // + Order doesn't matter - no indexing, unordered collection
    private ImmutableHashSet <ISubscriber <TContext>> subscribers;
    
    // -- constructors
    public Publisher()
        // TODO: #3
        => this.subscribers = ImmutableHashSet <ISubscriber <TContext>>.Empty;

    public Publisher (ICollection <ISubscriber <TContext>> subscribers)
    // thread safe is not necessary on a field that no other thread can see yet, since the object
    // isn't published. 
        => this.subscribers = ImmutableHashSet.CreateRange(subscribers);

    // -- implemented interfaces
    public void Subscribe (ISubscriber <TContext> subscriber)
        => ImmutableInterlocked.Update(ref this.subscribers, s
            => s.Add(subscriber));

    public void Unsubscribe (ISubscriber <TContext> subscriber)
        => ImmutableInterlocked.Update(ref this.subscribers, s
            => s.Remove(subscriber));

    public void NotifySubscribers (TContext context) {
        var occuredException = new PublisherException();
        var snapshotSubscribers = this.subscribers;
        
        foreach (var snapshotSubscriber in snapshotSubscribers) {
            try {
                snapshotSubscriber.Notify(context);
            } catch (Exception exception) {
                occuredException.Add(snapshotSubscriber, exception);
            }
        }

        if (occuredException.HasErrors)
        // no notification has an impact on an other notification, the feedback is given after each
        // subscriber was notified
            throw occuredException;
    }
}