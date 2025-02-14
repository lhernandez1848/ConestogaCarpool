USE [master]
GO
/****** Object:  Database [ConestogaCarpool]    Script Date: 2019-11-21 8:26:21 PM ******/
CREATE DATABASE [ConestogaCarpool]
GO
USE [ConestogaCarpool]
GO
/****** Object:  Table [dbo].[Driver]    Script Date: 2019-11-21 8:26:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Driver](
	[driverId] [int] IDENTITY(1,1) NOT NULL,
	[userId] [int] NOT NULL,
	[licenceClassId] [int] NOT NULL,
	[experience] [int] NOT NULL,
 CONSTRAINT [PK_Driver] PRIMARY KEY CLUSTERED 
(
	[driverId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LicenceClass]    Script Date: 2019-11-21 8:26:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LicenceClass](
	[licenceClassId] [int] IDENTITY(1,1) NOT NULL,
	[licenceClass] [varchar](5) NOT NULL,
 CONSTRAINT [PK_LicenceClass] PRIMARY KEY CLUSTERED 
(
	[licenceClassId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Post]    Script Date: 2019-11-21 8:26:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Post](
	[postId] [int] IDENTITY(1,1) NOT NULL,
	[postStatusId] [int] NOT NULL,
	[driverId] [int] NOT NULL,
	[vehicleId] [int] NOT NULL,
	[destination] [varchar](255) NOT NULL,
	[location] [varchar](255) NOT NULL,
	[date] [date] NOT NULL,
	[time] [time](7) NOT NULL,
 CONSTRAINT [PK_Post] PRIMARY KEY CLUSTERED 
(
	[postId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PostStatus]    Script Date: 2019-11-21 8:26:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PostStatus](
	[postStatusId] [int] IDENTITY(1,1) NOT NULL,
	[postStatusDescription] [varchar](15) NOT NULL,
 CONSTRAINT [PK_PostStatus] PRIMARY KEY CLUSTERED 
(
	[postStatusId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Request]    Script Date: 2019-11-21 8:26:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Request](
	[requestId] [int] IDENTITY(1,1) NOT NULL,
	[requestStatusId] [int] NOT NULL,
	[passengerId] [int] NOT NULL,
	[postId] [int] NOT NULL,
 CONSTRAINT [PK_Request] PRIMARY KEY CLUSTERED 
(
	[requestId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RequestStatus]    Script Date: 2019-11-21 8:26:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RequestStatus](
	[requestStatusId] [int] IDENTITY(1,1) NOT NULL,
	[requestStatusDescription] [varchar](15) NOT NULL,
 CONSTRAINT [PK_RequestStatus] PRIMARY KEY CLUSTERED 
(
	[requestStatusId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Review]    Script Date: 2019-11-21 8:26:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Review](
	[reviewId] [int] IDENTITY(1,1) NOT NULL,
	[rating] [int] NOT NULL,
	[comment] [varchar](255) NOT NULL,
	[rideId] [int] NOT NULL,
	[passengerId] [int] NOT NULL,
	[driverId] [int] NOT NULL,
 CONSTRAINT [PK_Review] PRIMARY KEY CLUSTERED 
(
	[reviewId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Ride]    Script Date: 2019-11-21 8:26:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ride](
	[rideId] [int] IDENTITY(1,1) NOT NULL,
	[rideStatusId] [int] NOT NULL,
	[postId] [int] NOT NULL,
	[requestId] [int] NOT NULL,
 CONSTRAINT [PK_Ride] PRIMARY KEY CLUSTERED 
(
	[rideId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RideStatus]    Script Date: 2019-11-21 8:26:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RideStatus](
	[rideStatusId] [int] IDENTITY(1,1) NOT NULL,
	[rideStatusDescription] [varchar](15) NOT NULL,
 CONSTRAINT [PK_RideStatus] PRIMARY KEY CLUSTERED 
(
	[rideStatusId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 2019-11-21 8:26:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[userId] [int] IDENTITY(1,1) NOT NULL,
	[username] [varchar](255) NOT NULL,
	[password] [nvarchar](255) NOT NULL,
	[firstName] [varchar](255) NOT NULL,
	[lastName] [varchar](255) NOT NULL,
	[email] [varchar](255) NOT NULL,
	[verifiedEmail] [varchar](3) NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[userId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserImage]    Script Date: 2019-11-21 8:26:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserImage](
	[userImageId] [int] IDENTITY(1,1) NOT NULL,
	[userId] [int] NOT NULL,
	[image] [varbinary](max) NOT NULL,
 CONSTRAINT [PK_UserImage] PRIMARY KEY CLUSTERED 
(
	[userImageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Vehicle]    Script Date: 2019-11-21 8:26:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Vehicle](
	[vehicleId] [int] IDENTITY(1,1) NOT NULL,
	[userId] [int] NOT NULL,
	[make] [varchar](255) NOT NULL,
	[model] [varchar](255) NOT NULL,
	[year] [int] NOT NULL,
	[colour] [varchar](255) NOT NULL,
	[plate] [varchar](8) NULL,
 CONSTRAINT [PK_Vehicle] PRIMARY KEY CLUSTERED 
(
	[vehicleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VehicleImage]    Script Date: 2019-11-21 8:26:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VehicleImage](
	[vehicleImageId] [int] IDENTITY(1,1) NOT NULL,
	[vehicleId] [int] NOT NULL,
	[image] [varbinary](max) NOT NULL,
 CONSTRAINT [PK_VehicleImage] PRIMARY KEY CLUSTERED 
(
	[vehicleImageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Driver] ON 

INSERT [dbo].[Driver] ([driverId], [userId], [licenceClassId], [experience]) VALUES (1, 1, 1, 4)
INSERT [dbo].[Driver] ([driverId], [userId], [licenceClassId], [experience]) VALUES (2, 2, 2, 2)
INSERT [dbo].[Driver] ([driverId], [userId], [licenceClassId], [experience]) VALUES (3, 3, 1, 5)
INSERT [dbo].[Driver] ([driverId], [userId], [licenceClassId], [experience]) VALUES (4, 7, 1, 3)
INSERT [dbo].[Driver] ([driverId], [userId], [licenceClassId], [experience]) VALUES (6, 6, 2, 2)
INSERT [dbo].[Driver] ([driverId], [userId], [licenceClassId], [experience]) VALUES (7, 4, 1, 5)
SET IDENTITY_INSERT [dbo].[Driver] OFF
SET IDENTITY_INSERT [dbo].[LicenceClass] ON 

INSERT [dbo].[LicenceClass] ([licenceClassId], [licenceClass]) VALUES (1, N'G')
INSERT [dbo].[LicenceClass] ([licenceClassId], [licenceClass]) VALUES (2, N'G2')
SET IDENTITY_INSERT [dbo].[LicenceClass] OFF
SET IDENTITY_INSERT [dbo].[Post] ON 

INSERT [dbo].[Post] ([postId], [postStatusId], [driverId], [vehicleId], [destination], [location], [date], [time]) VALUES (1, 1, 1, 1, N'Kitchener', N'Waterloo', CAST(N'2019-11-01' AS Date), CAST(N'10:00:00' AS Time))
INSERT [dbo].[Post] ([postId], [postStatusId], [driverId], [vehicleId], [destination], [location], [date], [time]) VALUES (2, 1, 3, 2, N'Kitchener', N'Waterloo', CAST(N'2019-11-04' AS Date), CAST(N'09:30:00' AS Time))
INSERT [dbo].[Post] ([postId], [postStatusId], [driverId], [vehicleId], [destination], [location], [date], [time]) VALUES (3, 1, 3, 2, N'Kitchener', N'Waterloo', CAST(N'2019-11-05' AS Date), CAST(N'13:00:00' AS Time))
INSERT [dbo].[Post] ([postId], [postStatusId], [driverId], [vehicleId], [destination], [location], [date], [time]) VALUES (4, 1, 4, 3, N'Kitchener', N'Guelph', CAST(N'2019-11-04' AS Date), CAST(N'11:30:00' AS Time))
INSERT [dbo].[Post] ([postId], [postStatusId], [driverId], [vehicleId], [destination], [location], [date], [time]) VALUES (5, 1, 4, 3, N'Guelph', N'Kitchener', CAST(N'2019-11-04' AS Date), CAST(N'15:00:00' AS Time))
INSERT [dbo].[Post] ([postId], [postStatusId], [driverId], [vehicleId], [destination], [location], [date], [time]) VALUES (8, 1, 6, 5, N'Waterloo', N'Kitchener', CAST(N'2019-11-05' AS Date), CAST(N'10:00:00' AS Time))
INSERT [dbo].[Post] ([postId], [postStatusId], [driverId], [vehicleId], [destination], [location], [date], [time]) VALUES (9, 1, 6, 5, N'Kitchener', N'Waterloo', CAST(N'2019-11-05' AS Date), CAST(N'16:00:00' AS Time))
SET IDENTITY_INSERT [dbo].[Post] OFF
SET IDENTITY_INSERT [dbo].[PostStatus] ON 

INSERT [dbo].[PostStatus] ([postStatusId], [postStatusDescription]) VALUES (1, N'Available')
INSERT [dbo].[PostStatus] ([postStatusId], [postStatusDescription]) VALUES (2, N'Unavailable')
SET IDENTITY_INSERT [dbo].[PostStatus] OFF
SET IDENTITY_INSERT [dbo].[Request] ON 

INSERT [dbo].[Request] ([requestId], [requestStatusId], [passengerId], [postId]) VALUES (1, 4, 4, 2)
SET IDENTITY_INSERT [dbo].[Request] OFF
SET IDENTITY_INSERT [dbo].[RequestStatus] ON 

INSERT [dbo].[RequestStatus] ([requestStatusId], [requestStatusDescription]) VALUES (1, N'Accepted')
INSERT [dbo].[RequestStatus] ([requestStatusId], [requestStatusDescription]) VALUES (2, N'Declined')
INSERT [dbo].[RequestStatus] ([requestStatusId], [requestStatusDescription]) VALUES (3, N'Expired')
INSERT [dbo].[RequestStatus] ([requestStatusId], [requestStatusDescription]) VALUES (4, N'Pending')
SET IDENTITY_INSERT [dbo].[RequestStatus] OFF
SET IDENTITY_INSERT [dbo].[Review] ON 

INSERT [dbo].[Review] ([reviewId], [rating], [comment], [rideId], [passengerId], [driverId]) VALUES (3, 5, N'testing ', 2, 4, 4)
SET IDENTITY_INSERT [dbo].[Review] OFF
SET IDENTITY_INSERT [dbo].[Ride] ON 

INSERT [dbo].[Ride] ([rideId], [rideStatusId], [postId], [requestId]) VALUES (1, 1, 1, 1)
INSERT [dbo].[Ride] ([rideId], [rideStatusId], [postId], [requestId]) VALUES (2, 2, 5, 1)
INSERT [dbo].[Ride] ([rideId], [rideStatusId], [postId], [requestId]) VALUES (3, 3, 8, 1)
SET IDENTITY_INSERT [dbo].[Ride] OFF
SET IDENTITY_INSERT [dbo].[RideStatus] ON 

INSERT [dbo].[RideStatus] ([rideStatusId], [rideStatusDescription]) VALUES (1, N'In Progress')
INSERT [dbo].[RideStatus] ([rideStatusId], [rideStatusDescription]) VALUES (2, N'Completed')
INSERT [dbo].[RideStatus] ([rideStatusId], [rideStatusDescription]) VALUES (3, N'Cancelled')
SET IDENTITY_INSERT [dbo].[RideStatus] OFF
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([userId], [username], [password], [firstName], [lastName], [email], [verifiedEmail]) VALUES (1, N'test', N'AHhB1cW8bNJKuD9ZGdyoUfdMfNETm8JzaazWj6uGnPjNuihphdBxmMZDB1cAxROgKw==', N'Test', N'TestLastName', N'test@conestogac.on.ca', N'yes')
INSERT [dbo].[User] ([userId], [username], [password], [firstName], [lastName], [email], [verifiedEmail]) VALUES (2, N'coolkid', N'AC3k062xN+xyqVUL84pOaoScrkq6/9eovN/kPcSbE4cvnVexczEVZUjV3lvSP9l+sQ==', N'Conan', N'Edogawa', N'coolkid@mailinator.com', N'yes')
INSERT [dbo].[User] ([userId], [username], [password], [firstName], [lastName], [email], [verifiedEmail]) VALUES (3, N'aread', N'AD4wDNUggrgmuRN3lAyR92m5b/E6sWJ2SWmfRN0TVUmdwhfriZcefiOnk3zZE2Qjyw==', N'Arthur', N'Read', N'aread1357@conestogac.on.ca', N'yes')
INSERT [dbo].[User] ([userId], [username], [password], [firstName], [lastName], [email], [verifiedEmail]) VALUES (4, N'bbaxter', N'AA7Ikv+oZaFJRrkGmfQd6JBNb1vtgqBZUO3I3vxVr+CN/ltXoiIKgd9ZfKIEZnehzg==', N'Buster', N'Baxter', N'bbaxter2468@conestogac.on.ca', N'yes')
INSERT [dbo].[User] ([userId], [username], [password], [firstName], [lastName], [email], [verifiedEmail]) VALUES (5, N'ffrensky', N'AJnak8UZ3yC25PbW1wEcRENshGU1M5Fpb1vMO7d7oNxqht5O/IU9aFrz0k62iUBWEg==', N'Francine', N'Frensky', N'ffrensky3579@conestogac.on.ca', N'yes')
INSERT [dbo].[User] ([userId], [username], [password], [firstName], [lastName], [email], [verifiedEmail]) VALUES (6, N'mcrosswire', N'AGThVOTzWhEqJSdFtuz32S2Zf7f779RtUSrf1zVLBnuBZ056WEVyKcobggF/NX/tOA==', N'Muffy', N'Crosswire', N'mcrosswire4680@conestogac.on.ca', N'yes')
INSERT [dbo].[User] ([userId], [username], [password], [firstName], [lastName], [email], [verifiedEmail]) VALUES (7, N'bpowers', N'AI54zXiVciFLwPzwlEaCCSh1cdr8gRtfAqPj21w9rvATRaS8hl9JlDWMRcPnoDkWbg==', N'Brian', N'Powers', N'bpowers1470@conestogac.on.ca', N'yes')
INSERT [dbo].[User] ([userId], [username], [password], [firstName], [lastName], [email], [verifiedEmail]) VALUES (8, N'abarnes', N'AGugMHDNnUHGM0PakXkysghIeSMvBPP5tmynDdj2Lcq0BUkXxL09dGIp0DLC4Uep4A==', N'Angela', N'Barnes', N'abarnes2581@conestogac.on.ca', N'yes')
INSERT [dbo].[User] ([userId], [username], [password], [firstName], [lastName], [email], [verifiedEmail]) VALUES (9, N'sarmstrong', N'ABKMfX8+fey45mtw89LU1Y2zf0ae2uybyYTWdtvVdoO2BMDTWPh6kPpNZZJ9HrLbLg==', N'Sue', N'Armstrong', N'sarmstrong3692@conestogac.on.ca', N'yes')
INSERT [dbo].[User] ([userId], [username], [password], [firstName], [lastName], [email], [verifiedEmail]) VALUES (10, N'fwalters', N'ANHMGUlBNzbO72jZhCdFfe/HLyGZj3CEfTjUzokHSOfnft9HaZX6DYBTJtxgho1fPw==', N'Fern', N'Walters', N'fwalters4703@conestogac.on.ca', N'yes')
SET IDENTITY_INSERT [dbo].[User] OFF
SET IDENTITY_INSERT [dbo].[Vehicle] ON 

INSERT [dbo].[Vehicle] ([vehicleId], [userId], [make], [model], [year], [colour], [plate]) VALUES (1, 1, N'Chevrolet', N'Cruze', 2017, N'Black', N'ABCD123')
INSERT [dbo].[Vehicle] ([vehicleId], [userId], [make], [model], [year], [colour], [plate]) VALUES (2, 3, N'Toyota', N'Corolla', 2019, N'Black', N'WERT135')
INSERT [dbo].[Vehicle] ([vehicleId], [userId], [make], [model], [year], [colour], [plate]) VALUES (3, 7, N'Honda', N'Civic', 2018, N'Silver', N'EFGH456')
INSERT [dbo].[Vehicle] ([vehicleId], [userId], [make], [model], [year], [colour], [plate]) VALUES (4, 10, N'Hyundai', N'Tucson', 2019, N'Black', N'HJKL798')
INSERT [dbo].[Vehicle] ([vehicleId], [userId], [make], [model], [year], [colour], [plate]) VALUES (5, 6, N'Honda', N'Fit', 2017, N'Blue', N'CVBN741')
INSERT [dbo].[Vehicle] ([vehicleId], [userId], [make], [model], [year], [colour], [plate]) VALUES (10, 1, N'Nissan', N'Sentra', 2015, N'White', N'DONKEY11')
SET IDENTITY_INSERT [dbo].[Vehicle] OFF
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__User__AB6E6164EFFB08F3]    Script Date: 2019-11-21 8:26:21 PM ******/
ALTER TABLE [dbo].[User] ADD UNIQUE NONCLUSTERED 
(
	[email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__User__F3DBC572852FBDE9]    Script Date: 2019-11-21 8:26:21 PM ******/
ALTER TABLE [dbo].[User] ADD UNIQUE NONCLUSTERED 
(
	[username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Driver]  WITH CHECK ADD  CONSTRAINT [FK_Driver_LicenceClass] FOREIGN KEY([licenceClassId])
REFERENCES [dbo].[LicenceClass] ([licenceClassId])
GO
ALTER TABLE [dbo].[Driver] CHECK CONSTRAINT [FK_Driver_LicenceClass]
GO
ALTER TABLE [dbo].[Driver]  WITH CHECK ADD  CONSTRAINT [FK_Driver_User] FOREIGN KEY([userId])
REFERENCES [dbo].[User] ([userId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Driver] CHECK CONSTRAINT [FK_Driver_User]
GO
ALTER TABLE [dbo].[Post]  WITH CHECK ADD  CONSTRAINT [FK_Post_Driver] FOREIGN KEY([driverId])
REFERENCES [dbo].[Driver] ([driverId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Post] CHECK CONSTRAINT [FK_Post_Driver]
GO
ALTER TABLE [dbo].[Post]  WITH CHECK ADD  CONSTRAINT [FK_Post_PostStatus] FOREIGN KEY([postStatusId])
REFERENCES [dbo].[PostStatus] ([postStatusId])
GO
ALTER TABLE [dbo].[Post] CHECK CONSTRAINT [FK_Post_PostStatus]
GO
ALTER TABLE [dbo].[Post]  WITH CHECK ADD  CONSTRAINT [FK_Post_Vehicle] FOREIGN KEY([vehicleId])
REFERENCES [dbo].[Vehicle] ([vehicleId])
GO
ALTER TABLE [dbo].[Post] CHECK CONSTRAINT [FK_Post_Vehicle]
GO
ALTER TABLE [dbo].[Request]  WITH CHECK ADD  CONSTRAINT [FK_Request_Post] FOREIGN KEY([postId])
REFERENCES [dbo].[Post] ([postId])
GO
ALTER TABLE [dbo].[Request] CHECK CONSTRAINT [FK_Request_Post]
GO
ALTER TABLE [dbo].[Request]  WITH CHECK ADD  CONSTRAINT [FK_Request_RequestStatus] FOREIGN KEY([requestStatusId])
REFERENCES [dbo].[RequestStatus] ([requestStatusId])
GO
ALTER TABLE [dbo].[Request] CHECK CONSTRAINT [FK_Request_RequestStatus]
GO
ALTER TABLE [dbo].[Request]  WITH CHECK ADD  CONSTRAINT [FK_Request_User] FOREIGN KEY([passengerId])
REFERENCES [dbo].[User] ([userId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Request] CHECK CONSTRAINT [FK_Request_User]
GO
ALTER TABLE [dbo].[Review]  WITH CHECK ADD  CONSTRAINT [FK_Review_Driver] FOREIGN KEY([driverId])
REFERENCES [dbo].[Driver] ([driverId])
GO
ALTER TABLE [dbo].[Review] CHECK CONSTRAINT [FK_Review_Driver]
GO
ALTER TABLE [dbo].[Review]  WITH CHECK ADD  CONSTRAINT [FK_Review_Ride] FOREIGN KEY([rideId])
REFERENCES [dbo].[Ride] ([rideId])
GO
ALTER TABLE [dbo].[Review] CHECK CONSTRAINT [FK_Review_Ride]
GO
ALTER TABLE [dbo].[Review]  WITH CHECK ADD  CONSTRAINT [FK_Review_User] FOREIGN KEY([passengerId])
REFERENCES [dbo].[User] ([userId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Review] CHECK CONSTRAINT [FK_Review_User]
GO
ALTER TABLE [dbo].[Ride]  WITH CHECK ADD  CONSTRAINT [FK_Ride_Post] FOREIGN KEY([postId])
REFERENCES [dbo].[Post] ([postId])
GO
ALTER TABLE [dbo].[Ride] CHECK CONSTRAINT [FK_Ride_Post]
GO
ALTER TABLE [dbo].[Ride]  WITH CHECK ADD  CONSTRAINT [FK_Ride_Request] FOREIGN KEY([requestId])
REFERENCES [dbo].[Request] ([requestId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Ride] CHECK CONSTRAINT [FK_Ride_Request]
GO
ALTER TABLE [dbo].[Ride]  WITH CHECK ADD  CONSTRAINT [FK_Ride_RideStatus] FOREIGN KEY([rideStatusId])
REFERENCES [dbo].[RideStatus] ([rideStatusId])
GO
ALTER TABLE [dbo].[Ride] CHECK CONSTRAINT [FK_Ride_RideStatus]
GO
ALTER TABLE [dbo].[UserImage]  WITH CHECK ADD  CONSTRAINT [FK_UserImage_User] FOREIGN KEY([userId])
REFERENCES [dbo].[User] ([userId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserImage] CHECK CONSTRAINT [FK_UserImage_User]
GO
ALTER TABLE [dbo].[Vehicle]  WITH CHECK ADD  CONSTRAINT [FK_Vehicle_User] FOREIGN KEY([userId])
REFERENCES [dbo].[User] ([userId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Vehicle] CHECK CONSTRAINT [FK_Vehicle_User]
GO
ALTER TABLE [dbo].[VehicleImage]  WITH CHECK ADD  CONSTRAINT [FK_VehicleImage_Vehicle] FOREIGN KEY([vehicleId])
REFERENCES [dbo].[Vehicle] ([vehicleId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[VehicleImage] CHECK CONSTRAINT [FK_VehicleImage_Vehicle]
GO
USE [master]
GO
ALTER DATABASE [ConestogaCarpool] SET  READ_WRITE 
GO
