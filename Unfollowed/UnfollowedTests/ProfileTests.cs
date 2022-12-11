using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Unfollowed;

namespace UnfollowedTests
{
    [TestClass]
    public class ProfileTests
    {
        [TestMethod]
        public void TestLoadingFiles()
        {
            //Profile testProfile = new Profile(
            //    "trav",
            //    "travv.ts",
            //    "C:\\Users\\travi\\source\\repos\\InstaFollowedMe\\InstaFollowedMe\\SourceFiles\\Followers.txt",
            //    "C:\\Users\\travi\\source\\repos\\InstaFollowedMe\\InstaFollowedMe\\SourceFiles\\Following.txt");

            //testProfile.Update("C:\\Users\\travi\\source\\repos\\InstaFollowedMe\\InstaFollowedMe\\Profiles\\NewFollowers.txt",
            //    "C:\\Users\\travi\\source\\repos\\InstaFollowedMe\\InstaFollowedMe\\Profiles\\NewFollowing.txt");
            //string[] whoDoesntFollowBack = testProfile.GetNotFollowedBack();
            //string[] whoUnfollowedMe = testProfile.GetUnfollowedBy();
            //string[] WhoIdontFollow = testProfile.GetNotFollowingBack();
            //string[] NewFollowers = testProfile.GetNewFollowers();
            //string[] NewFollowing = testProfile.GetNewFollowing();
            //string[] whoIunfollowed = testProfile.GetUnfollowed();
            //testProfile.SaveProfile();
            //Array.Sort(whoDoesntFollowBack);
            //Assert.IsTrue(true);
        }

        [TestMethod]
        public void TestReadProfile()
        {
            Profile testprofile = new Profile("travv.ts");
            testprofile.Update("C:\\Users\\travi\\source\\repos\\InstaFollowedMe\\InstaFollowedMe\\Profiles\\NewFollowers.txt",
            "C:\\Users\\travi\\source\\repos\\InstaFollowedMe\\InstaFollowedMe\\Profiles\\NewFollowing.txt");
            string[] NewFollowers = testprofile.GetNewFollowers();
            string[] NewFollowing = testprofile.GetNewFollowing();
            string[] WhoIdontFollow = testprofile.GetNotFollowingBack();
            string[] WhoUnfollowedMe = testprofile.GetUnfollowedBy();
            testprofile.SaveProfile();
            Assert.IsTrue(true);
        }
    }
}
