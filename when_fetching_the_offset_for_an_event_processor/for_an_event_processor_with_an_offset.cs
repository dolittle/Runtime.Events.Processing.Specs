// Copyright (c) Dolittle. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Dolittle.Runtime.Events.Store;
using Machine.Specifications;

namespace Dolittle.Runtime.Events.Processing.InMemory.Specs.when_fetching_the_offset_for_an_event_processor
{
    [Subject(typeof(IEventProcessorOffsetRepository), nameof(IEventProcessorOffsetRepository.Get))]
    public class for_an_event_processor_with_an_offset : given.an_offset_repository
    {
        static IEventProcessorOffsetRepository repository;
        static CommittedEventVersion result;
        static EventProcessorId id;
        static CommittedEventVersion last_version;

        Establish context = () =>
        {
            id = Guid.NewGuid();
            last_version = new CommittedEventVersion(10, 5, 1);
            repository = get_offset_repository();
            _do(repository, _ => _.Set(id, last_version));
        };

        Because of = () => _do(repository, (_) => result = _.Get(id));

        It should_return_the_none_commit_version = () => result.ShouldEqual(last_version);
        Cleanup cleanup = () => repository.Dispose();
    }
}