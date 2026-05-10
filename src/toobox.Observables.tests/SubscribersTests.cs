using toolbox.Observables;
using Xunit;

namespace toobox.Observables.tests;

public class SubscribersTests {

    // -- act
    [Fact]
    public void ExpectContextFromNotify() {

        var context = "12.05.2004";
        var subscriber = new SubscriberSpyHelper<string>();
        
        subscriber.Subscriber.Notify(context);
        Assert.Equal(context, subscriber.LatestContextByNotify);
    }

    // -- helper
    internal class SubscriberSpyHelper <TContext> {

        public TContext? LatestContextByNotify { get; private set; }
        public readonly Subscriber <TContext> Subscriber;
        
        // -- constructor
        public SubscriberSpyHelper()
            => this.Subscriber = new(this.NotifyCallback);
        
        // -- methods
        private void NotifyCallback (TContext context)
            => this.LatestContextByNotify = context;
    }

}