using ConstradeApi.Entity;
using ConstradeApi.Model.MOtp;
using ConstradeApi.Model.MProduct;
using ConstradeApi.Model.MSubcription;
using ConstradeApi.Model.MTransaction;
using ConstradeApi.Model.MUser;
using ConstradeApi.Model.MUserApiKey;
using ConstradeApi.Model.MUserChat;
using ConstradeApi.Model.MUserMessage;
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
        
    }
}
