﻿using System;
using System.Collections.Generic;
using FluentAssertions;
using LinkDotNet.Blog.Web.RegistrationExtensions;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace LinkDotNet.Blog.UnitTests;

public class StorageProviderRegistrationExtensionsTests
{
    public static IEnumerable<object[]> Data => new List<object[]>
        {
            new object[] { new Action<IServiceCollection>(services => services.UseSqliteAsStorageProvider()) },
            new object[] { new Action<IServiceCollection>(services => services.UseSqlAsStorageProvider()) },
            new object[] { new Action<IServiceCollection>(services => services.UseInMemoryAsStorageProvider()) },
            new object[] { new Action<IServiceCollection>(services => services.UseRavenDbAsStorageProvider()) },
        };

    [Theory]
    [MemberData(nameof(Data))]
    public void GivenAlreadyRegisteredRepository_WhenTryingToAddAnotherStorage_ThenException(Action<IServiceCollection> act)
    {
        var services = new ServiceCollection();
        services.UseRavenDbAsStorageProvider();

        Action actualAct = () => act(services);

        actualAct.Should().Throw<NotSupportedException>();
    }
}
