using ConstradeApi.Entity;
using ConstradeApi.Model.MCommunity;
using ConstradeApi.Model.MCommunity.MCommunityJoinRequest;
using ConstradeApi.Model.MCommunity.MCommunityMember;
using ConstradeApi.Model.MOtp;
using ConstradeApi.Model.MProduct;
using ConstradeApi.Model.MProductChat;
using ConstradeApi.Model.MProductMessage;
using ConstradeApi.Model.MReport;
using ConstradeApi.Model.MSubcription;
using ConstradeApi.Model.MSystemFeedback;
using ConstradeApi.Model.MTransaction;
using ConstradeApi.Model.MUser;
using ConstradeApi.Model.MUserApiKey;
using ConstradeApi.Model.MUserChat;
using ConstradeApi.Model.MUserMessage;
using ConstradeApi.Model.MUserNotification;
using ConstradeApi.Model.MWallet;

namespace ConstradeApi.Services.EntityToModel
{
    public static class EntityCastService
    {
        public static PersonModel ToModel(this Person person)
        {
            return new PersonModel()
            {
                PersonId = person.Person_id,
                AddressReferenceId = person.AddressRef_id,
                FirstName = person.FirstName,
                LastName = person.LastName,
                Birthdate = person.Birthdate,
                PhoneNumber = person.PhoneNumber,
                Gender = person.Gender,
            };
        }
        public static UserModel ToModel(this User user)
        {
            return new UserModel()
            {
                UserId = user.UserId,
                FirebaseId = user.FirebaseId,
                PersonRefId = user.PersonRefId,
                UserType = user.UserType,
                AuthProviderType = user.AuthProviderType,
                UserStatus = user.UserStatus,
                Email = user.Email,
                ImageUrl = user.ImageUrl,
                LastActiveAt = user.LastActiveAt,
                CountPost = user.CountPost,
                DateCreated = user.DateCreated
            };
        }
        public static ProductModel ToModel(this Product product)
        {
            return new ProductModel()
            {
                ProductId = product.ProductId,
                PosterUserId = product.PosterUserId,
                Title = product.Title,
                Description = product.Description,
                ModelNumber = product.ModelNumber,
                SerialNumber = product.SerialNumber,
                GameGenre = product.GameGenre,
                Platform = product.Platform,
                ThumbnailUrl = product.ThumbnailUrl,
                Cash = product.Cash,
                Item = product.Item,
                DateCreated = product.DateCreated,
                CountFavorite = product.CountFavorite,
                Condition = product.Condition,
                PreferTrade = product.PreferTrade,
                DeliveryMethod = product.DeliveryMethod,
                Location = product.Location,
                ProductStatus = product.ProductStatus,
                HasReceipts = product.HasReceipts,
                HasWarranty = product.HasWarranty,
            };
        }
        public static ProductCommentModel ToModel(this ProductComment comment)
        {
            return new ProductCommentModel
            {
                ProductCommentId = comment.ProductCommentId,
                Comment = comment.Comment,
                UserId = comment.UserId,
                ProductId = comment.ProductId,
                DateCreated = comment.DateCreated,
            };
        }
        public static FavoriteModel ToModel(this Favorites favorite)
        {
            return new FavoriteModel()
            {
                FavoriteId = favorite.FavoriteId,
                UserId = favorite.UserId,
                ProductId = favorite.ProductId,
                Date = favorite.Date,
            };
        }
        public static UserFollowModel ToModel(this Follow follow)
        {
            return new UserFollowModel()
            {
                FollowId = follow.FollowId,
                FollowByUserId = follow.FollowByUserId,
                FollowedByUserId = follow.FollowedByUserId,
                DateFollowed = follow.DateFollowed
            };
        }
        public static TransactionModel ToModel(this Transaction transaction)
        {
            return new TransactionModel
            {
                TransactionId = transaction.TransactionId,
                ProductId = transaction.ProductId,
                BuyerUserId = transaction.BuyerUserId,
                SellerUserId = transaction.SellerUserId,
                InAppTransaction = transaction.InAppTransaction,
                GetWanted = transaction.GetWanted,
                IsReviewed = transaction.IsReviewed,
                DateTransaction = transaction.DateTransaction,
            };
        }
        public static UserReviewModel ToModel(this Review review) 
        {
            return new UserReviewModel
            {
                ReviewId = review.ReviewId,
                TransactionRefId = review.TransactionRefId,
                Rate = review.Rate,
                DateCreated = review.DateCreated,
            };
        }
        public static WalletModel ToModel(this Wallet wallet)
        {
            return new WalletModel()
            {
                WalletId = wallet.WalletId,
                UserId = wallet.UserId,
                Balance = wallet.Balance,
            };
        }
        public static TopUpTransactionModel ToModel(this TopUpTransaction transac)
        {
            return new TopUpTransactionModel()
            {
                TopUpTransactionId = transac.TopUpTransactionId,
                WalletId = transac.WalletId,
                Amount = transac.Amount,
                DateTopUp = transac.DateTopUp,
            };
        }
        public static SendMoneyTransactionModel ToModel(this SendMoneyTransaction transac)
        {
            return new SendMoneyTransactionModel()
            {
                SendMoneyTransactionId = transac.SendMoneyTransactionId,
                SenderWalletId = transac.SenderWalletId,
                ReceiverWalletId = transac.ReceiverWalletId,
                Amount = transac.Amount,
                DateSend = transac.DateSend,
            };
        }
        public static SubscriptionModel ToModel(this Subscription subscription)
        {
            return new SubscriptionModel
            {
                SubscriptionId = subscription.SubscriptionId,
                UserId = subscription.UserId,
                SubscriptionType = subscription.SubscriptionType,
                DateStart = subscription.DateStart,
                DateEnd = subscription.DateEnd,
                Amount = subscription.Amount,
            };
        }
        public static SubscriptionHistoryModel ToModel(this SubscriptionHistory subscription)
        {
            return new SubscriptionHistoryModel
            {
                SubscriptionHistoryId = subscription.SubscriptionHistoryId,
                SubscriptionId = subscription.SubscriptionId,
                DateUpdate = subscription.DateUpdate,
                PreviousSubscriptionType = subscription.PreviousSubscriptionType,
                NewSubscriptionType = subscription.NewSubscriptionType,
                PreviousDateStart = subscription.PreviousDateStart,
                NewDateStart = subscription.NewDateStart,
                PreviousDateEnd = subscription.PreviousDateEnd,
                NewDateEnd = subscription.NewDateEnd,
                PreviousAmount = subscription.PreviousAmount,
                NewAmount = subscription.NewAmount,
            };
        }
        public static OtpModel ToModel(this OneTimePassword otp)
        {
            return new OtpModel()
            {
                OtpId = otp.OtpId,
                OTP = otp.OTP,
                SendTo = otp.SendTo,
                ExpirationTime = otp.ExpirationTime
            };
        }
        public static ApiKeyModel ToModel(this ApiKey apiKey)
        {
            return new ApiKeyModel
            {
                ApiKeyId = apiKey.ApiKeyId,
                UserId = apiKey.UserId,
                Token = apiKey.Token,
                DateCreated = apiKey.DateCreated,
                IsActive = apiKey.IsActive,
            };
        }
        public static UserChatModel ToModel(this UserChat userChat)
        {
            return new UserChatModel()
            {
                UserChatId = userChat.UserChatId,
                UserId1= userChat.UserId1,
                UserId2= userChat.UserId2,
                LastMessage= userChat.LastMessage,
                LastMessageDate= userChat.LastMessageDate,
            };
        }
        public static UserMessageModel ToModel(this UserMessage userMessage)
        {
            return new UserMessageModel()
            {
                UserMessageId = userMessage.UserMessageId,
                UserChatId = userMessage.UserChatId,
                SenderId = userMessage.SenderId,
                Message = userMessage.Message,
                DateSent = userMessage.DateSent,

            };
        }
        public static ImageListModel ToModel(this ImageList imageList)
        {
            return new ImageListModel()
            {
                ImageId = imageList.ImageId,
                ProductId = imageList.ProductId,
                ImageURL = imageList.ImageURL,
            };
        }
        public static ProductChatModel ToModel(this ProductChat productChat)
        {
            return new ProductChatModel()
            {
                ProductChatId = productChat.ProductChatId,
                UserId1 = productChat.UserId1,
                UserId2= productChat.UserId2,
                ProductId = productChat.ProductId,
                LastMessage = productChat.LastMessage,
                LastMessageDate = productChat.LastMessageDate,
                ChatCreated = productChat.ChatCreated,
            };
        }
        public static ProductMessageModel ToModel(this ProductMessage productMessage)
        {
            return new ProductMessageModel()
            {
                ProductMessageId= productMessage.ProductMessageId,
                ProductChatId = productMessage.ProductChatId,
                SenderId = productMessage.SenderId,
                Message = productMessage.Message,
                DateSent = productMessage.DateSent,
            };
        }
        public static ReportModel ToModel(this Report report)
        {
            return new ReportModel
            {
                ReportId = report.ReportId,
                IdReported= report.IdReported,
                ReportedBy = report.ReportedBy,
                ReportType = report.ReportType,
                Description = report.Description,
                Status = report.Status,
                DateSubmitted = report.DateSubmitted,
            };
        }
        public static SystemFeedbackModel ToModel(this SystemFeedback systemFeedback)
        {
            return new SystemFeedbackModel
            {
                SystemFeedbackId = systemFeedback.SystemFeedbackId,
                UserId = systemFeedback.UserId,
                ReportType = systemFeedback.ReportType,
                Title = systemFeedback.Title,
                Status = systemFeedback.Status,
                Description = systemFeedback.Description,
                DateSubmitted = systemFeedback.DateSubmitted,
            };
        }
        public static UserNotificationModel ToModel(this UserNotification notification)
        {
            return new UserNotificationModel
            {
                UserNotificationId = notification.UserNotificationId,
                NotificationType = notification.NotificationType,
                NotificationMessage = notification.NotificationMessage,
                NotificationDate = notification.NotificationDate,
                ImageUrl = notification.ImageUrl,
                UserId = notification.UserId,
                ToId = notification.ToId,
            };
        }
        public static CommunityModel ToModel(this Community community)
        {
            return new CommunityModel
            {
                CommunityId = community.CommunityId,
                OwnerUserId = community.OwnerUserId,
                Name = community.Name,
                Description = community.Description,
                ImageUrl = community.ImageUrl,
                DateCreated = community.DateCreated,
                TotalMembers = community.TotalMembers,
            };
        }
        public static CommunityMemberModel ToModel(this CommunityMember community)
        {
            return new CommunityMemberModel
            {
                CommunityMemberId = community.CommunityMemberId,
                UserId= community.UserId,
                CommunityId = community.CommunityId,
                Role= community.Role,
                MemberSince = community.MemberSince,
            };
        }
        public static CommunityJoinModel ToModel(this CommunityJoin info)
        {
            return new CommunityJoinModel
            {
                CommunityJoinRequestId = info.CommunityJoinRequestId,
                CommunityId = info.CommunityId,
                UserId = info.UserId,
                Status = info.Status,
                DateRequested = info.DateRequested,

            };
        }
    }
}
