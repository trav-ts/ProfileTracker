/*
 * Author: Travis Slade
 * Date: 12/09/2022
 * Notes: 
 *      Created with the intention of tracking an instagram profile. More specifically,
 *      The followers and following of an instagram profile. But, it could be used 
 *      for any platform that supports followers and followings.
 * */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unfollowed
{
    /// <summary>
    /// 
    /// </summary>
    public class Profile
    {
        /// <summary>
        /// Full name of the profile. Could be the same as other profiles.
        /// </summary>
        public string Name { 
            get { return Name; } 
            private set { Name = value; } 
        }

        /// <summary>
        /// Username for the profile. Must be a unique identifier. 
        /// </summary>
        public string UserName {
            get { return UserName; }
            private set { UserName = value; }
        }

        /// <summary>
        /// Date containing the last time the profile has been updated.
        /// Regardless of whether or not changes were detected.
        /// </summary>
        public DateTime LastUpdate{
            get { return LastUpdate; }
            private set { LastUpdate = value; }
        }
        
        public int Followers {
            get { return Followers; }
            private set { Followers = value; }
        }

        public int Following {
            get { return Following; }
            private set { Following = value; }
        }

        // Hold the last know list of followers and following.
        private string[] CurrFollowers;
        private string[] CurrFollowing;

        // Hold the new followers and following. Could be same list
        // as old if no changes to profile.
        private string[] NewFollowers;
        private string[] NewFollowing;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="userName"></param>
        public Profile(string name, string userName)
        {
            this.Name = name;
            this.UserName = userName;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="userName"></param>
        /// <param name="followers"></param>
        /// <param name="following"></param>
        public Profile(string name, string userName, string[] followers, string[] following)
        {
            this.Name = name;
            this.UserName = userName;
            CurrFollowers = followers;
            CurrFollowing = following;
        }

        
        public Profile(string name, string userName, string followersPath, string followingPath)
        {
            this.Name = name;
            this.UserName = userName;
            
            // NEED TO DO: 
            // Read followers and following from the file paths provided.
        }

        /// <summary>
        /// Save the profile to a local xml file. Name, Username, followers, 
        /// and following will be saved.
        /// 
        /// If the method fails to save, it will return false. (Maybe it should throw error) TBD
        /// </summary>
        /// <returns>True if saved, false otherwise</returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool SaveProfile()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Read a previously save profile from the provided filepath.
        /// 
        /// NOTE: If profile is created with other data, it will be overriden!
        /// </summary>
        /// <param name="filePath">Location of desired profile</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool ReadProfile(string filePath)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Compare the current list of following to the new list of following
        /// and return the usernames that are no longer in the new list.
        /// This will be the users that this profile has unfollwed.
        /// 
        /// </summary>
        /// <returns>List of users this profile is no longer following</returns>
        /// <exception cref="NotImplementedException"></exception>
        public string[] GetUnfollowed()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Compare the old list of followers to the new list of followers. 
        /// Usernames that appear only in the old list will be saved as users
        /// who unfollowed this profile.
        /// 
        /// It is possible that said user was removed by this profile but it is
        /// assumed that the owner of this profile will be aware of that.
        /// 
        /// </summary>
        /// <returns>List of usernames who no longer follow this profile</returns>
        /// <exception cref="NotImplementedException"></exception>
        public string[] GetUnfollowedBy()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get the list of usernames that this profile is not following back.
        /// 
        /// </summary>
        /// <returns>List of usernames not following back</returns>
        /// <exception cref="NotImplementedException"></exception>
        public string[] GetNotFollowingBack()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get the list of users that are not following this profile back.
        /// </summary>
        /// <returns>List of usernames not following this profile.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public string[] GetNotFollowedBack()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets a list of usernames that this profile has followed since
        /// the last update.
        /// 
        /// </summary>
        /// <returns>List of usernames that are new followers</returns>
        /// <exception cref="NotImplementedException"></exception>
        public string[] GetNewFollowers()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets a list of new usernames this profile is following 
        /// since last update.
        /// 
        /// </summary>
        /// <returns>List of new usernames</returns>
        /// <exception cref="NotImplementedException"></exception>
        public string[] GetNewFollowing()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Provides new lists of followers and followings to update this profile with. 
        /// 
        /// </summary>
        /// <param name="followersPath">new followers</param>
        /// <param name="followingPath">new following</param>
        /// <returns>true if succesful update</returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool Update(string followersPath, string followingPath)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Two profile are considered equal if they have the same username. 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            return this.UserName == (string)obj;
        }

        public override int GetHashCode()
        {
            return this.UserName.GetHashCode();
        }
    }
}
