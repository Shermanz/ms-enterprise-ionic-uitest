using System;
using System.Linq;
using System.Threading;
using FluentAssertions;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Android;

namespace MSEnterprise.Test.UI
{
    [TestFixture]
    public class Tests
    {
        AndroidApp app;

        [SetUp]
        public void BeforeEachTest()
        {
            app = ConfigureApp
                .Android
                .ApkFile("../../../ms-enterprise/platforms/android/build/outputs/apk/android-debug.apk")
                .StartApp();
        }

        [Test]
        public void DeveNavegarParaTelaChats()
        {
            NavegarParaTelaChats();
            var title = app.WaitForElement(x => x.WebView().Css("ion-header-bar .title"));

            title.FirstOrDefault().TextContent.Should().Be("Chats");
        }

        [Test]
        public void DeveNavegarParaTelaAccounts()
        {
            NavegarParaTelaAccounts();
            var title = app.WaitForElement(x => x.WebView().Css("ion-header-bar .title"));

            title.FirstOrDefault().TextContent.Should().Be("Account");
        }

        [Test]
        public void DeveDesativarAmigos()
        {
            NavegarParaTelaAccounts();

            app.Tap(x => x.WebView().Css(".toggle"));
            var labelFriends = app.WaitForElement(x => x.WebView().Css(".enableFriends span.ng-binding")).FirstOrDefault().TextContent;

            labelFriends.Should().Contain("Enable Friends");
        }

        [Test]
        public void DeveClicarNoUltimoElementoDaLista()
        {
            var ultimoElementoDaLista = "#user-8";

            NavegarParaTelaChats();

            app.ScrollDownTo(x => x.WebView().Css(ultimoElementoDaLista), strategy: ScrollStrategy.Gesture, timeout: new TimeSpan(0, 0, 30));
            app.Tap(x => x.WebView().Css(ultimoElementoDaLista));

            var title = app.WaitForElement(x => x.WebView().Css(".title.title-left.header-item")).FirstOrDefault().TextContent;

            title.Should().Be("Wennder dos Santos");
        }

        private void NavegarParaTelaAccounts()
        {
            app.Tap(x => x.WebView().Css(".tab-item").Index(2));
        }

        private void NavegarParaTelaChats()
        {
            app.Tap(x => x.WebView().Css(".tab-item").Index(1));

        }
    }
}

