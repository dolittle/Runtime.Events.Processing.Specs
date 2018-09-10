namespace Dolittle.Runtime.Events.Processing.InMemory.Specs.when_setting_the_offset_for_an_event_processor
{
    using System;
    using Dolittle.Runtime.Events.Processing;
    using Dolittle.Runtime.Events.Store;
    using Machine.Specifications;

    [Subject(typeof(IEventProcessorOffsetRepository), nameof(IEventProcessorOffsetRepository.Set))]
    public class and_it_is_a_new_processor : given.an_offset_repository
    {
        static IEventProcessorOffsetRepository repository;
        static CommittedEventVersion last_processed;
        static EventProcessorId event_processor;
        
        Establish context = () => 
        {
            last_processed = new CommittedEventVersion(100,1,1);
            event_processor = Guid.NewGuid();
            repository = get_offset_repository();
        };

        Because of = () => _do(repository, _ => _.Set(event_processor,last_processed));

        It should_set_the_last_processed_version_for_the_processor = () => repository.Get(event_processor).ShouldEqual(last_processed);
        Cleanup cleanup = () => repository.Dispose();
    }
}