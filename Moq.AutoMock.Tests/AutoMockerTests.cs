﻿using Should;
using Xunit;

namespace Moq.AutoMock.Tests
{
    public class AutoMockerTests
    {
        #region Types used for testing

        public class Empty
        {
        }

        public class OneConstructor
        {
            public Empty Empty;

            public OneConstructor(Empty empty)
            {
                this.Empty = empty;
            }
        }

        #endregion

        public class DescribeGetInstance
        {
            private readonly AutoMocker mocker = new AutoMocker();

            [Fact]
            public void It_creates_object_with_no_constructor()
            {
                var instance = mocker.GetInstance<Empty>();
                instance.ShouldNotBeNull();
            }

            [Fact]
            public void It_creates_objects_for_ctor_parameters()
            {
                var instance = mocker.GetInstance<OneConstructor>();
                instance.Empty.ShouldNotBeNull();
            }

            [Fact]
            public void It_creates_mock_objects_for_ctor_parameters()
            {
                var instance = mocker.GetInstance<OneConstructor>();
                Mock.Get(instance.Empty).ShouldNotBeNull();
            }
        }

        public class DescribeUsingExplicitObjects
        {
            private readonly AutoMocker mocker = new AutoMocker();

            [Fact]
            public void You_can_Use_an_instance_as_an_argument_to_GetInstance()
            {
                var empty = new Empty();
                mocker.Use(empty);
                var instance = mocker.GetInstance<OneConstructor>();
                instance.Empty.ShouldBeSameAs(empty);
            }
        }
    }
}
