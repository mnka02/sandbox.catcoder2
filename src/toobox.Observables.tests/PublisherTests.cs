using toolbox.Observables;
using Xunit;

namespace toobox.Observables.tests;

public class PublisherTests {

    // test object
    private readonly Publisher <int> publisher;

    // test data set
    public static IEnumerable <object[]> SubscriberListData
        = new List <object[]>() {
            new object[] {
                new object[] {
                    SubscriberOverviewHelper.CreateOverview("Cody"),
                    SubscriberOverviewHelper.CreateOverview("Rex"),
                    SubscriberOverviewHelper.CreateOverview("Fox")
                }
            }
        };

    // test data set, each subscriber provokes an exception when notified
    public static IEnumerable <object[]> FaultySubscriberListData = new List <object[]>() {
        new object[] { FaultySubscriberOverviewHelper.CreateOverview("Bly") }
    };

    // test data set, each subscriber demands uniqueness in subscriber list 
    public static IEnumerable <object[]> UniqueSubscriberListData = new List <object[]>() {
        new object[] { UniqueSubscriberOverviewHelper.CreateOverview("Boil") }
    };

    public PublisherTests()
        => this.publisher = new();

    // -- act
    [Theory]
    [MemberData(nameof(SubscriberListData))]
    public void ExpectRegisteredSubscriberIsNotified(SubscriberOverviewHelper[] overviews) {
        // act
        this.publisher.Subscribe(overviews[0]);
        this.publisher.NotifySubscribers(12052004);
        // assert
        Assert.True(overviews[0].HasSubscribed);
    }

    [Theory]
    [MemberData(nameof(UniqueSubscriberListData))]
    public void ExpectRegisteredSubscriberIsUnique (SubscriberOverviewHelper overview) {
        // act
        this.publisher.Subscribe(overview);
        this.publisher.NotifySubscribers(01012024);
        // assert
        Assert.Throws <PublisherException>(() =>
        {
            this.publisher.NotifySubscribers(31122026);
        });
    }

    [Theory]
    [MemberData(nameof(SubscriberListData))]
    public void ExpectSubscriberIsIgnoredAfterUnsubscribe (SubscriberOverviewHelper[] overviews) {
        // act
        this.publisher.Subscribe(overviews[0]);
        this.publisher.Unsubscribe(overviews[0]);
        this.publisher.NotifySubscribers(26092025);
        // assert
        Assert.False(overviews[0].HasSubscribed);
        Assert.Null(overviews[0].LatestContext);
    }

    [Theory]
    [MemberData(nameof(SubscriberListData))]
    public void ExpectUnregisteredSubscriberIsIrrelevantToUnsubscribe(SubscriberOverviewHelper[] overviews) {
        this.publisher.Unsubscribe(overviews[0]);
    }

    [Theory]
    [MemberData(nameof(SubscriberListData))]
    public void ExpectEveryRegisteredSubscriberToReceiveContext (SubscriberOverviewHelper[] overviews) {
        // act
        foreach (var overview in overviews)
            this.publisher.Subscribe(overview);
        var context = 07012026;
        this.publisher.NotifySubscribers(context);
        
        // assert
        foreach (var overview in overviews)
            Assert.Equal(overview.LatestContext, context);
    }

    [Theory]
    [MemberData(nameof(SubscriberListData))]
    public void ExpectFailedIssuationDoesNotBlock (SubscriberOverviewHelper[] overviews) {
        var split = 1;
        this.publisher.Subscribe(FaultySubscriberOverviewHelper.CreateOverview("Gree"));
        for (int i = 0; i <= split; i++)
            this.publisher.Subscribe(overviews[i]);

        var context = 11072002;
        try {
            this.publisher.NotifySubscribers(context);
            Assert.Fail("No failed issuation, illegal test conditions!");
        } catch (Exception exception) {
            for (int i = 0; i <= split; i++)
                Assert.True(overviews[i].LatestContext == context);
        }
    }

    [Theory]
    [MemberData(nameof(FaultySubscriberListData))]
    public void ExpectFeedbackAfterCompleteIssuiation(SubscriberOverviewHelper faultyOverview) {
        // act
        this.publisher.Subscribe(faultyOverview);
        // assert
        Assert.Throws <PublisherException>(() => {
            this.publisher.NotifySubscribers(17051949);
        });
    }

    [Theory]
    [MemberData(nameof(SubscriberListData))]
    public void ExpectNoFeedbackAfterCompleteIssuation (SubscriberOverviewHelper[] overviews) {
        this.publisher.Subscribe(overviews[0]);
        this.publisher.NotifySubscribers(29101969);
    }
    
    // -- helper classes
    
    /// <summary>
    /// Mocks a subscriber based on the ISubscriber interface. 
    /// </summary>
    public class SubscriberHelper :
        ISubscriber <int>,
        IComparable <SubscriberHelper>, IEquatable <SubscriberHelper> {

        public readonly string Id;

        public SubscriberHelper (string id)
            => this.Id = id;
        
        // -- implemented interfaces
        public void Notify (int context) {
            throw new NotImplementedException();
        }

        public int CompareTo (SubscriberHelper? other)
            // TODO: #4
            // String.CompareTo is culture-specific!
            // he comparison follows language/locale rules that vary by region, rather than comparing raw character values
            => String.CompareOrdinal(this.Id, other?.Id);

        public bool Equals (SubscriberHelper? other)
            => this.Id.Equals(other?.Id);
    }

    /// <summary>
    /// Mocks a subscriber, based on the ISubscriber interface. In addition it spys on every notification
    /// that is issued by a publisher instance.
    /// </summary>
    public class SubscriberOverviewHelper: ISubscriber <int> {

        public readonly SubscriberHelper Subscriber;
        // overview 
        public bool HasSubscribed { get; set; } = false;
        public int? LatestContext { get; set; } = null;
        
        // -- constructor
        public SubscriberOverviewHelper (SubscriberHelper subscriber)
            => this.Subscriber = subscriber;
        
        // -- methods
        /// <summary>
        /// Factory
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static SubscriberOverviewHelper CreateOverview (string id) {
            var factoredSubscriber = new SubscriberHelper(id);
            var factoredOverview = new SubscriberOverviewHelper(factoredSubscriber);
            return factoredOverview;
        }

        // -- implemented methods
        public virtual void Notify (int context) {
            this.HasSubscribed = true;
            this.LatestContext = context;
        }
    }
    
    /// <summary>
    /// Mocks a subscriber, based on the ISubscriber interface. In addition it spys on every notification
    /// that is issued by a publisher instance. Everytime the it is notified by a publisher an Exception is
    /// thrown.
    /// </summary>
    public class FaultySubscriberOverviewHelper: SubscriberOverviewHelper {
        
        public FaultySubscriberOverviewHelper(SubscriberHelper subscriber) 
            : base(subscriber) { }

        /// <summary>
        /// Factory
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static FaultySubscriberOverviewHelper CreateOverview (string id) {
            var factoredSubscriber = new SubscriberHelper(id);
            var factoredOverview = new FaultySubscriberOverviewHelper(factoredSubscriber);
            return factoredOverview;
        }
        
        // -- overrides
        public override void Notify (int context) {
            base.Notify(context);
            throw new Exception("Notify failed here!");
        }
    }

    /// <summary>
    /// Mocks a subscriber, based on the ISubscriber interface. In addition it spys on every notification
    /// that is issued by a publisher instance. The instance demands uniqueness of its underlaying mocked
    /// subscriber. Uniqueness is certified by only one subscription call during runtime.
    /// </summary>
    public class UniqueSubscriberOverviewHelper : SubscriberOverviewHelper {
        
        public UniqueSubscriberOverviewHelper(SubscriberHelper subscriber) 
            : base(subscriber) { }

        public static UniqueSubscriberOverviewHelper CreateOverview (string id) {
            var factoredSubscriber = new SubscriberHelper(id);
            var factoredOverview = new UniqueSubscriberOverviewHelper(factoredSubscriber);
            return factoredOverview;
        }
        
        // -- overrides
        public override void Notify (int context) {
            if (base.HasSubscribed)
                throw new Exception("Multiple subscribes!");
            base.Notify(context);
        }
    }
}