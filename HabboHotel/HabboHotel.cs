﻿using System;

using Aleeda.Storage;
using Aleeda.Net.Connections;
using Aleeda.Specialized.Utilities;

using Aleeda.HabboHotel.Client;
using Aleeda.HabboHotel.Habbos;
using Aleeda.HabboHotel.Messenger;
using Aleeda.HabboHotel.Achievements;
using Aleeda.HabboHotel.Catalog;
using Aleeda.HabboHotel.Rooms;
using Aleeda.HabboHotel;

namespace Aleeda.HabboHotel
{
    /// <summary>
    /// Represents a multiuser, virtual hotel where users can create avatars, to chat with other users in spaces set up as 'rooms'. Users have the ability to create their own 'guestroom' as well, and they are able to decorate it with virtual furniture.
    /// </summary>
    public class HabboHotel
    {
        #region Fields
        private uint mVersion;

        // Modules
        private KeyValueDictionary mExternalTexts = null;
        private KeyValueDictionary mExternalVariables = null;

        private GameClientManager mClientManager = null;
        private HabboManager mHabboManager = null;
        private HabboAuthenticator mAuthenticator = null;
        //private UserRightManager mUserRightManager = null;
        private AchievementManager mAchievementManager = null;
        private MessengerManager mMessengerManager = null;
        private Catalog.Catalog mCatalog;
        private RoomUser mRoomUser;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the version of the game client as a 32 bit unsigned integer.
        /// </summary>
        public uint Version
        {
            get { return mVersion; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Constructs a Habbo Hotel environment and tries to initialize it.
        /// </summary>
        public HabboHotel()
        {
            // Try to parse version
            AleedaEnvironment.Configuration.TryParseUInt32("projects.habbo.client.version", out mVersion);
            
            // Initialize HabboHotel project modules
            mExternalTexts = new KeyValueDictionary("external_texts", "xkey", "xvalue");
            mExternalVariables = new KeyValueDictionary("external_variables", "xkey", "xvalue");
            mClientManager = new GameClientManager();
            mHabboManager = new HabboManager();
            mAuthenticator = new HabboAuthenticator();
            mAchievementManager = new AchievementManager();
            mMessengerManager = new MessengerManager();
            mCatalog = new Catalog.Catalog();
            mRoomUser = new RoomUser();

            // Start connection checker for clients
            mClientManager.StartConnectionChecker();

            // Load external texts and external variables
            mExternalTexts.Reload();
            mExternalVariables.Reload();


            // Print that we are done!
            Console.WriteLine(string.Format(" [**] --> Initialized project 'Habbo Hotel' for version R{0}.", mVersion));
        }
        #endregion

        #region Methods
        public void Destroy()
        {
            // Clear clients
            if (GetClients() != null)
            {
                GetClients().Clear();
                GetClients().StopConnectionChecker();
            }

            Console.WriteLine(string.Format("Destroyed project 'Habbo Hotel' for version {0}.", mVersion));
        }

        /// <summary>
        /// Returns the game client manager.
        /// </summary>
        public GameClientManager GetClients()
        {
            return mClientManager;
        }
        /// <summary>
        /// Returns the Habbo manager.
        /// </summary>
        public HabboManager GetHabbos()
        {
            return mHabboManager;
        }
        /// <summary>
        /// Returns the Habbo authenticator.
        /// </summary>
        public HabboAuthenticator GetAuthenticator()
        {
            return mAuthenticator;
        }
        /// <summary>
        /// Returns the AchievementManager instance.
        /// </summary>
        public AchievementManager GetAchievements()
        {
            return mAchievementManager;
        }
        /// <summary>
        /// Returns the MessengerManager instance.
        /// </summary>
        public MessengerManager GetMessenger()
        {
            return mMessengerManager;
        }
        /// <summary>
        /// Returns the RoomId instance.
        /// </summary>
        public Catalog.Catalog GetCatalog()
        {
            return mCatalog;
        }
        /// <summary>
        /// Returns the Catalog instance.
        /// </summary>
        public RoomUser GetRoomUser()
        {
            return mRoomUser;
        }
        #endregion
    }
}
