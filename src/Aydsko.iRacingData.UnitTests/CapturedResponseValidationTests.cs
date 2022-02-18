﻿// © 2022 Adrian Clark
// This file is licensed to you under the MIT license.

namespace Aydsko.iRacingData.UnitTests;

public class CapturedResponseValidationTests : MockedHttpTestBase
{
    // NUnit will ensure that "SetUp" runs before each test so these can all be forced to "null".
    private iRacingDataClient sut = null!;

    [SetUp]
    public void SetUp()
    {
        BaseSetUp();
        sut = new iRacingDataClient(HttpClient, new TestLogger<iRacingDataClient>());
    }

    [Test(TestOf = typeof(iRacingDataClient))]
    public async Task ValidCredentialsIsSuccessfulAsync()
    {
        await MessageHandler.QueueResponsesAsync(nameof(ValidCredentialsIsSuccessfulAsync)).ConfigureAwait(false);

        await sut.LoginAsync("test.user@example.com", "SuperSecretPassword", CancellationToken.None).ConfigureAwait(false);

        Assert.That(MessageHandler.Requests.TryDequeue(out var request), Is.True, "Somehow no request was made.");
        try
        {
            Assert.That(request, Is.Not.Null);

            Assert.That(request!.RequestUri?.AbsoluteUri, Is.EqualTo("https://members-ng.iracing.com/auth"));
            var content = await (request.Content?.ReadAsStringAsync() ?? Task.FromResult(string.Empty)).ConfigureAwait(false);
            Assert.That(content, Is.Not.Null.Or.Empty.And.Contains("test.user@example.com").And.Contains("SuperSecretPassword"));

            Assert.That(sut.IsLoggedIn, Is.True, "IsLoggedIn property should reflect state of authentication");
        }
        finally
        {
            request?.Dispose();
        }
    }

    [Test(TestOf = typeof(iRacingDataClient))]
    public async Task GetCarAssetDetailsSuccessfulAsync()
    {
        await MessageHandler.QueueResponsesAsync(nameof(GetCarAssetDetailsSuccessfulAsync)).ConfigureAwait(false);
        await sut.LoginAsync("test.user@example.com", "SuperSecretPassword", CancellationToken.None).ConfigureAwait(false);

        var carAssets = await sut.GetCarAssetDetailsAsync().ConfigureAwait(false);

        Assert.That(carAssets, Is.Not.Null);
        Assert.That(carAssets!.Data, Is.Not.Null);

        Assert.That(carAssets.Data, Has.Count.EqualTo(125));
        Assert.That(carAssets.RateLimitRemaining, Is.EqualTo(99));
        Assert.That(carAssets.TotalRateLimit, Is.EqualTo(100));
        Assert.That(carAssets.RateLimitReset, Is.EqualTo(new DateTimeOffset(2022, 2, 10, 0, 0, 0, TimeSpan.Zero)));
    }

    [Test(TestOf = typeof(iRacingDataClient))]
    public async Task GetCarsSuccessfulAsync()
    {
        await MessageHandler.QueueResponsesAsync(nameof(GetCarsSuccessfulAsync)).ConfigureAwait(false);
        await sut.LoginAsync("test.user@example.com", "SuperSecretPassword", CancellationToken.None).ConfigureAwait(false);

        var cars = await sut.GetCarsAsync().ConfigureAwait(false);

        Assert.That(cars, Is.Not.Null);
        Assert.That(cars!.Data, Is.Not.Null);

        Assert.That(cars.Data, Has.Length.EqualTo(125));
        Assert.That(cars.RateLimitRemaining, Is.EqualTo(99));
        Assert.That(cars.TotalRateLimit, Is.EqualTo(100));
        Assert.That(cars.RateLimitReset, Is.EqualTo(new DateTimeOffset(2022, 2, 10, 0, 0, 0, TimeSpan.Zero)));
    }

    [Test(TestOf = typeof(iRacingDataClient))]
    public async Task GetCarClassesSuccessfulAsync()
    {
        await MessageHandler.QueueResponsesAsync(nameof(GetCarClassesSuccessfulAsync)).ConfigureAwait(false);
        await sut.LoginAsync("test.user@example.com", "SuperSecretPassword", CancellationToken.None).ConfigureAwait(false);

        var carClasses = await sut.GetCarClassesAsync().ConfigureAwait(false);

        Assert.That(carClasses, Is.Not.Null);
        Assert.That(carClasses!.Data, Is.Not.Null);

        Assert.That(carClasses.Data, Has.Length.EqualTo(161));
        Assert.That(carClasses.RateLimitRemaining, Is.EqualTo(99));
        Assert.That(carClasses.TotalRateLimit, Is.EqualTo(100));
        Assert.That(carClasses.RateLimitReset, Is.EqualTo(new DateTimeOffset(2022, 2, 10, 0, 0, 0, TimeSpan.Zero)));
    }

    [Test(TestOf = typeof(iRacingDataClient))]
    public async Task GetLookupsSuccessfulAsync()
    {
        await MessageHandler.QueueResponsesAsync(nameof(GetLookupsSuccessfulAsync)).ConfigureAwait(false);
        await sut.LoginAsync("test.user@example.com", "SuperSecretPassword", CancellationToken.None).ConfigureAwait(false);

        var lookupGroups = await sut.GetLookupsAsync().ConfigureAwait(false);

        Assert.That(lookupGroups, Is.Not.Null);
        Assert.That(lookupGroups!.Data, Is.Not.Null);

        Assert.That(lookupGroups.Data, Has.Length.EqualTo(2));
        Assert.That(lookupGroups.RateLimitRemaining, Is.EqualTo(99));
        Assert.That(lookupGroups.TotalRateLimit, Is.EqualTo(100));
        Assert.That(lookupGroups.RateLimitReset, Is.EqualTo(new DateTimeOffset(2022, 2, 10, 0, 0, 0, TimeSpan.Zero)));
    }

    [Test(TestOf = typeof(iRacingDataClient))]
    public async Task GetLicensesSuccessfulAsync()
    {
        await MessageHandler.QueueResponsesAsync(nameof(GetLicensesSuccessfulAsync)).ConfigureAwait(false);
        await sut.LoginAsync("test.user@example.com", "SuperSecretPassword", CancellationToken.None).ConfigureAwait(false);

        var carAssets = await sut.GetLicensesAsync().ConfigureAwait(false);

        Assert.That(carAssets, Is.Not.Null);
        Assert.That(carAssets!.Data, Is.Not.Null);

        Assert.That(carAssets.Data, Has.Length.EqualTo(7));
        Assert.That(carAssets.RateLimitRemaining, Is.EqualTo(99));
        Assert.That(carAssets.TotalRateLimit, Is.EqualTo(100));
        Assert.That(carAssets.RateLimitReset, Is.EqualTo(new DateTimeOffset(2022, 2, 10, 0, 0, 0, TimeSpan.Zero)));
    }

    [Test(TestOf = typeof(iRacingDataClient))]
    public async Task GetDriverInfoWithLicensesSuccessfulAsync()
    {
        await MessageHandler.QueueResponsesAsync(nameof(GetDriverInfoWithLicensesSuccessfulAsync)).ConfigureAwait(false);
        await sut.LoginAsync("test.user@example.com", "SuperSecretPassword", CancellationToken.None).ConfigureAwait(false);

        var carAssets = await sut.GetDriverInfoAsync(new[] {123456}, true).ConfigureAwait(false);

        Assert.That(carAssets, Is.Not.Null);
        Assert.That(carAssets!.Data, Is.Not.Null);

        Assert.That(carAssets.Data, Has.Length.EqualTo(1));
        Assert.That(carAssets.Data[0].Licenses, Is.Not.Null);
        Assert.That(carAssets.Data[0].Licenses, Has.Length.EqualTo(4));

        Assert.That(carAssets.RateLimitRemaining, Is.EqualTo(99));
        Assert.That(carAssets.TotalRateLimit, Is.EqualTo(100));
        Assert.That(carAssets.RateLimitReset, Is.EqualTo(new DateTimeOffset(2022, 2, 10, 0, 0, 0, TimeSpan.Zero)));
    }

    [Test(TestOf = typeof(iRacingDataClient))]
    public async Task GetDriverInfoWithoutLicensesSuccessfulAsync()
    {
        await MessageHandler.QueueResponsesAsync(nameof(GetDriverInfoWithoutLicensesSuccessfulAsync)).ConfigureAwait(false);
        await sut.LoginAsync("test.user@example.com", "SuperSecretPassword", CancellationToken.None).ConfigureAwait(false);

        var carAssets = await sut.GetDriverInfoAsync(new[] { 123456 }, false).ConfigureAwait(false);

        Assert.That(carAssets, Is.Not.Null);
        Assert.That(carAssets!.Data, Is.Not.Null);

        Assert.That(carAssets.Data, Has.Length.EqualTo(1));
        Assert.That(carAssets.Data[0].Licenses, Is.Null);
        Assert.That(carAssets.RateLimitRemaining, Is.EqualTo(99));
        Assert.That(carAssets.TotalRateLimit, Is.EqualTo(100));
        Assert.That(carAssets.RateLimitReset, Is.EqualTo(new DateTimeOffset(2022, 2, 10, 0, 0, 0, TimeSpan.Zero)));
    }

    [Test(TestOf = typeof(iRacingDataClient))]
    public async Task GetMemberInfoSucceedsAsync()
    {
        await MessageHandler.QueueResponsesAsync(nameof(GetMemberInfoSucceedsAsync)).ConfigureAwait(false);
        await sut.LoginAsync("test.user@example.com", "SuperSecretPassword", CancellationToken.None).ConfigureAwait(false);

        var myInfo = await sut.GetMyInfoAsync().ConfigureAwait(false);

        Assert.That(myInfo, Is.Not.Null);
        Assert.That(myInfo!.Data, Is.Not.Null);

        Assert.That(myInfo.RateLimitRemaining, Is.EqualTo(99));
        Assert.That(myInfo.TotalRateLimit, Is.EqualTo(100));
        Assert.That(myInfo.RateLimitReset, Is.EqualTo(new DateTimeOffset(2022, 2, 10, 0, 0, 0, TimeSpan.Zero)));
    }

    [Test(TestOf = typeof(iRacingDataClient))]
    public async Task GetMemberInfoDuringMaintenanceThrowsAsync()
    {
        await MessageHandler.QueueResponsesAsync(nameof(GetMemberInfoDuringMaintenanceThrowsAsync)).ConfigureAwait(false);
        await sut.LoginAsync("test.user@example.com", "SuperSecretPassword", CancellationToken.None).ConfigureAwait(false);

        Assert.ThrowsAsync<iRacingInMaintenancePeriodException>(async () => {
            var myInfo = await sut.GetMyInfoAsync().ConfigureAwait(false);
        });
    }
}