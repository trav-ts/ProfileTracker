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
using System.IO;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Xml;

namespace Unfollowed
{
    /// <summary>
    /// 
    /// </summary>
    public class Profile
    {
        private string name;
        private string userName;
        private int followers;
        private int following;
        private DateTime lastUpdate;
        /// <summary>
        /// Full name of the profile. Could be the same as other profiles.
        /// </summary>
        public string Name { 
            get { return name; } 
            private set { name = value; } 
        }

        /// <summary>
        /// Username for the profile. Must be a unique identifier. 
        /// </summary>
        public string UserName {
            get { return userName; }
            private set { userName = value; }
        }

        /// <summary>
        /// Date containing the last time the profile has been updated.
        /// Regardless of whether or not changes were detected.
        /// </summary>
        public DateTime LastUpdate{
            get { return lastUpdate; }
            private set { lastUpdate = value; }
        }
        
        public int Followers {
            get { return followers; }
            private set { followers = value; }
        }

        public int Following {
            get { return following; }
            private set { following = value; }
        }

        // Hold the last know list of followers and following.
        private string[] _currFollowers;
        private string[] _currFollowing;

        // Hold the new followers and following. Could be same list
        // as old if no changes to profile.
        private string[] _newFollowers;
        private string[] _newFollowing;

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
            _currFollowers = followers;
            _currFollowing = following;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="userName"></param>
        /// <param name="followersPath"></param>
        /// <param name="followingPath"></param>
        public Profile(string name, string userName, string followersPath, string followingPath)
        {
            this.Name = name;
            this.UserName = userName;

            this._currFollowers = ReadFile(followersPath);
            this._currFollowing = ReadFile(followingPath);
        }

        /// <summary>
        /// Save the profile to a local xml file. Name, Username, followers, 
        /// and following will be saved.
        /// 
        /// If the method fails to save, it will return false. (Maybe it should throw error) TBD
        /// </summary>
        /// <returns></returns
        public void SaveProfile()
        {
            XmlWriterSettings settings = new XmlWriterSettings { Indent = true };
            XmlWriter xmlWriter = XmlWriter.Create("C:\\Users\\travi\\source\\repos\\InstaFollowedMe\\InstaFollowedMe\\Profiles\\" + UserName + ".xml", settings);
            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("Profile");

            // Write name
            xmlWriter.WriteStartElement("Name");
            xmlWriter.WriteValue(Name);
            xmlWriter.WriteEndElement();

            // Write username
            xmlWriter.WriteStartElement("UserName");
            xmlWriter.WriteValue(UserName);
            xmlWriter.WriteEndElement();

            // Write followers
            foreach(string follower in _newFollowers ?? _currFollowers)
            {
                if (follower == null)
                    break;
                xmlWriter.WriteStartElement("Follower");
                xmlWriter.WriteValue(follower);
                xmlWriter.WriteEndElement();
            }

            // Write following
            foreach (string following in _newFollowing ?? _currFollowing)
            {
                if (following == null)
                    break;
                xmlWriter.WriteStartElement("Following");
                xmlWriter.WriteValue(following);
                xmlWriter.WriteEndElement();
            }
            xmlWriter.WriteEndElement();
            xmlWriter.WriteEndDocument();
            xmlWriter.Close();
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
        /// Parses a text file for usernames and returns that list.
        /// 
        /// Each "user" will take up at most 3 lines and at least 2 lines.
        /// Line 1 is the text name of the profile picture.
        /// Line 2 contains the userName.
        /// Line 3 contains the name. But it is not always present. 
        /// </summary>
        /// <param name="filePath">full path to .txt file</param>
        private string[] ReadFile(string filePath)
        {
            Regex reg = new Regex(@"^.*.(txt)$");
            if (!reg.IsMatch(filePath))
                throw new FileNotFoundException("File is not a .txt file");

            string[] lines = System.IO.File.ReadAllLines(filePath);

            // Size will be at most n/2 where n is the number of lines in the file.
            string[] userNames = new string[lines.Length / 2];

            int index = 0;

            for(int i = 0; i < lines.Length; i++)
            {
                if (lines[i].Contains("profile picture"))
                {
                    // Next line is gaurnteed to be a username.
                    userNames[index] = lines[i + 1];
                    index++;
                }
                else
                {
                    
                }
            }

            // Assign followers or following count since array length is not true representation of count.
            if (filePath.Contains("Followers"))
                Followers = index;
            else
                Following = index;
            return userNames;
        }

        /// <summary>
        /// Compare the current list of following to the new list of following
        /// and return the usernames that are no longer in the new list.
        /// This will be the users that this profile has unfollwed.
        /// 
        /// </summary>
        /// <returns>
        /// List of users this profile is no longer following.
        /// Empty list if newFollowers is null.
        /// </returns>
        public string[] GetUnfollowed()
        {
            return CompareLists(_currFollowing, _newFollowing);
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
        /// <returns>
        /// List of usernames who no longer follow this profile
        /// Empty list if newFollowers is null.
        /// </returns>
        public string[] GetUnfollowedBy()
        {
            return CompareLists(_currFollowers, _newFollowers);
        }

        /// <summary>
        /// Get the list of usernames that this profile is not following back.
        /// 
        /// </summary>
        /// <returns>List of usernames not following back</returns>
        public string[] GetNotFollowingBack()
        {
            if (_newFollowers == null)
                return CompareLists(_currFollowers, _currFollowing);
            else
                return CompareLists(_newFollowers, _newFollowing);
        }

        /// <summary>
        /// Get the list of users that are not following this profile back.
        /// </summary>
        /// <returns>List of usernames not following this profile.</returns>
        public string[] GetNotFollowedBack()
        {
            if (_newFollowers == null)
                return CompareLists(_currFollowing, _currFollowers);
            else
                return CompareLists(_newFollowing, _newFollowers);
        }

        /// <summary>
        /// Gets a list of usernames that this profile has followed since
        /// the last update.
        /// 
        /// </summary>
        /// <returns>List of usernames that are new followers</returns>
        public string[] GetNewFollowers()
        {
            return CompareLists(_newFollowers, _currFollowers);
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
            return CompareLists(_newFollowing, _currFollowing);
        }

        /// <summary>
        /// Provides new lists of followers and followings to update this profile with. 
        /// 
        /// </summary>
        /// <param name="followersPath">new followers full path</param>
        /// <param name="followingPath">new following full path</param>
        /// <returns></returns>
        public void Update(string followersPath, string followingPath)
        {
            this._newFollowers = ReadFile(followersPath);
            this._newFollowing = ReadFile(followingPath);
        }

        /// <summary>
        /// Gives the strings that exist in the first array but no in the second.
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        private string[] CompareLists(string[] first, string[] second)
        {
            if (first == null || second == null)
                return new string[0];
            
            IEnumerable<string> differenceQuery = first.Except(second);
            return differenceQuery.ToArray();
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
