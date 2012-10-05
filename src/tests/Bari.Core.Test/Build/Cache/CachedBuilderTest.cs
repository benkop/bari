﻿using System.Collections.Generic;
using Bari.Core.Build;
using Bari.Core.Build.Cache;
using Bari.Core.Generic;
using Bari.Core.Test.Helper;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace Bari.Core.Test.Build.Cache
{
    [TestFixture]
    public class CachedBuilderTest
    {
        [Test]
        public void RunsBuilderOnlyOnceIfFingerprintRemains()
        {
            // Setting up the test
            var resultSet = new HashSet<TargetRelativePath>
                {
                    new TargetRelativePath(@"a\b\c"), 
                    new TargetRelativePath(@"c\d"), 
                    new TargetRelativePath(@"e")
                };

            var realBuilder = new Mock<IBuilder>();
            var realBuilderDeps = new Mock<IDependencies>();
            var initialFingerprint = new Mock<IDependencyFingerprint>();
            var cache = new Mock<IBuildCache>();
            var targetDir = new TestFileSystemDirectory("target");

            realBuilderDeps.Setup(dep => dep.CreateFingerprint()).Returns(initialFingerprint.Object);
            
            realBuilder.Setup(b => b.Dependencies).Returns(realBuilderDeps.Object);
            realBuilder.Setup(b => b.Run()).Returns(resultSet);

            // Creating the builder
            var cachedBuilder = new CachedBuilder(realBuilder.Object, cache.Object, targetDir);

            cachedBuilder.Dependencies.Should().Be(realBuilderDeps.Object);            
            
            // Running the builder for the first time
            var result1 = cachedBuilder.Run();

            // ..verifying
            result1.Should().BeEquivalentTo(resultSet);            
            realBuilder.Verify(b => b.Run(), Times.Once());

            // Modifying cache behavior
            cache.Setup(c => c.Contains(realBuilder.Object.GetType(), initialFingerprint.Object)).Returns(true);
            cache.Setup(c => c.Restore(realBuilder.Object.GetType(), targetDir)).Returns(resultSet);

            // Running the builder for the second time
            var result2 = cachedBuilder.Run();

            // ..verifying
            result2.Should().BeEquivalentTo(resultSet);
            realBuilder.Verify(b => b.Run(), Times.Once());
        }
         
    }
}