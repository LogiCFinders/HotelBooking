using ezyvoyagerAPI.Data;
using ezyvoyagerAPI.Models.Domain;
using ezyvoyagerAPI.Models.DTOs;
using ezyvoyagerAPI.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Data.Entity;
using System.Linq.Expressions;


namespace ezyvoyagerAPI.Repositories.Implementation
{
    public class VoyagerRepository : IVoyagerRepository
    {
        private VoyagerDbContext _context;
        public VoyagerRepository()
        {
            _context = new VoyagerDbContext();

        }
        public VoyagerRepository(VoyagerDbContext voyagerDbContext)
        {
            _context = voyagerDbContext;

        }
        public async Task<VoyagerRESP> GetVoyagerItenary()
        {
            await Task.Delay(10);
            //byte[] iData = null;

            VoyagerInfo[] vinfo = _context.Voyager.ToArray();
            foreach (var v  in vinfo) {
                v.Itineraries = _context.Itinerary.Where(i => i.VoyagerID == v.VoyagerID).ToArray();
                v.InclusionsExclusions = _context.InclusionsExclusion.Where(x => x.VoyagerID == v.VoyagerID).ToArray();
                v.TravelDates = _context.TravelDate.Where(x => x.VoyagerID == v.VoyagerID).ToArray();
                v.Images = _context.Image.Where(x => x.VoyagerID == v.VoyagerID).ToArray();
                v.VoyagerFaqs = _context.VoyagerFAQ.Where(x => x.VoyagerID == v.VoyagerID).ToArray();

            }
            VoyagerRESP resp = new VoyagerRESP
            {
                Errors = new Errors { Code = "S001", Description = "Success",Message= "List of voyager Itenary" },
                VoyagerInfoArry = vinfo
            };
            return resp;
        }

        public async Task<VoyagerItenaryAddREQ> getVoyagerItenary(Int64 voyagerItenaryId)
        {
            VoyagerItenaryAddREQ voyagerItenaryAREQ= new VoyagerItenaryAddREQ();

            // Validate input (basic example — expand as needed)
            if (voyagerItenaryId <= 0)
            {

                voyagerItenaryAREQ.Errors = new Errors { Code = "Err000", Description = "voyagerItenaryId not provided", Message = "getVoyagerItenary  Request for voyagerItenaryId :: " + voyagerItenaryId };
                voyagerItenaryAREQ.Actions = VoyagerActionType.Get;
                return voyagerItenaryAREQ;
            }

            VoyagerInfo vg = _context.Voyager.Find(voyagerItenaryId);


            vg.Itineraries = _context.Itinerary.Where(i => i.VoyagerID == voyagerItenaryId).ToArray();
            vg.InclusionsExclusions=_context.InclusionsExclusion.Where(x=>x.VoyagerID== voyagerItenaryId).ToArray();
            vg.TravelDates= _context.TravelDate.Where(x => x.VoyagerID == voyagerItenaryId).ToArray();
            vg.Images= _context.Image.Where(x => x.VoyagerID == voyagerItenaryId).ToArray();
            vg.VoyagerFaqs= _context.VoyagerFAQ.Where(x => x.VoyagerID == voyagerItenaryId).ToArray();
           
           


            voyagerItenaryAREQ.Errors = new Errors { Code = "Err000", Description = "NO Error", Message = "Get Successfull" };
            return voyagerItenaryAREQ;
        }

        public async Task<VoyagerItenaryAddREQ> AddVoyagerItenary(VoyagerItenaryAddREQ voyagerItenaryAREQ)
        {


            // Validate input (basic example — expand as needed)
            if (voyagerItenaryAREQ.VoyagerInfoArry == null)
            {

                voyagerItenaryAREQ.Errors = new Errors { Code = "Err000", Description = "Data not provided", Message = "Addtion Request" };
                voyagerItenaryAREQ.Actions = VoyagerActionType.Add;
                return voyagerItenaryAREQ;
            }

            VoyagerInfo vg = new VoyagerInfo
            {
                Title = voyagerItenaryAREQ.VoyagerInfoArry[0].Title,
                Description = voyagerItenaryAREQ.VoyagerInfoArry[0].Description,
                GroupType = voyagerItenaryAREQ.VoyagerInfoArry[0].GroupType,
                Duration = voyagerItenaryAREQ.VoyagerInfoArry[0].Duration,
                DurationUnit = voyagerItenaryAREQ.VoyagerInfoArry[0].DurationUnit,
                Currency = voyagerItenaryAREQ.VoyagerInfoArry[0].Currency,
                Rating = voyagerItenaryAREQ.VoyagerInfoArry[0].Rating,
                RateHeading = voyagerItenaryAREQ.VoyagerInfoArry[0].RateHeading,
                PriceOffer = voyagerItenaryAREQ.VoyagerInfoArry[0].PriceOffer,
                CancellationPolicy = voyagerItenaryAREQ.VoyagerInfoArry[0].CancellationPolicy,
                PaymentPolicy = voyagerItenaryAREQ.VoyagerInfoArry[0].PaymentPolicy,
                Frequency = voyagerItenaryAREQ.VoyagerInfoArry[0].Frequency,
                CountryCode = voyagerItenaryAREQ.VoyagerInfoArry[0].CountryCode,
                PriceType = voyagerItenaryAREQ.VoyagerInfoArry[0].PriceType,
                MonthsOfSeasons = voyagerItenaryAREQ.VoyagerInfoArry[0].MonthsOfSeasons,
                BestTime = voyagerItenaryAREQ.VoyagerInfoArry[0].BestTime,
                OtherTerms = voyagerItenaryAREQ.VoyagerInfoArry[0].OtherTerms,
                ActivityType = voyagerItenaryAREQ.VoyagerInfoArry[0].ActivityType,
                PlaceOfActivity = voyagerItenaryAREQ.VoyagerInfoArry[0].PlaceOfActivity,
                Grade = voyagerItenaryAREQ.VoyagerInfoArry[0].Grade,
                StopSell = voyagerItenaryAREQ.VoyagerInfoArry[0].StopSell,
                Theme = voyagerItenaryAREQ.VoyagerInfoArry[0].Theme,
                DateCreated = voyagerItenaryAREQ.VoyagerInfoArry[0].DateCreated,
                ModifiedDate = voyagerItenaryAREQ.VoyagerInfoArry[0].ModifiedDate,
                MetaTags = voyagerItenaryAREQ.VoyagerInfoArry[0].MetaTags,
                CanonicalTags = voyagerItenaryAREQ.VoyagerInfoArry[0].CanonicalTags,
                Keywords = voyagerItenaryAREQ.VoyagerInfoArry[0].Keywords,
                SocialTags = voyagerItenaryAREQ.VoyagerInfoArry[0].SocialTags

            };

            // Finds by primary key (Id = 1)
            Int64 vId = 0;
            var vgr = _context.Voyager.Find(voyagerItenaryAREQ.VoyagerInfoArry[0].VoyagerID);


            if (vgr != null)
            {
                vgr.Title = voyagerItenaryAREQ.VoyagerInfoArry[0].Title;
                vgr.Description = voyagerItenaryAREQ.VoyagerInfoArry[0].Description;
                vgr.GroupType = voyagerItenaryAREQ.VoyagerInfoArry[0].GroupType;
                vgr.Duration = voyagerItenaryAREQ.VoyagerInfoArry[0].Duration;
                vgr.DurationUnit = voyagerItenaryAREQ.VoyagerInfoArry[0].DurationUnit;
                vgr.Currency = voyagerItenaryAREQ.VoyagerInfoArry[0].Currency;
                vgr.Rating = voyagerItenaryAREQ.VoyagerInfoArry[0].Rating;
                vgr.RateHeading = voyagerItenaryAREQ.VoyagerInfoArry[0].RateHeading;
                vgr.PriceOffer = voyagerItenaryAREQ.VoyagerInfoArry[0].PriceOffer;
                vgr.CancellationPolicy = voyagerItenaryAREQ.VoyagerInfoArry[0].CancellationPolicy;
                vgr.PaymentPolicy = voyagerItenaryAREQ.VoyagerInfoArry[0].PaymentPolicy;
                vgr.Frequency = voyagerItenaryAREQ.VoyagerInfoArry[0].Frequency;
                vgr.CountryCode = voyagerItenaryAREQ.VoyagerInfoArry[0].CountryCode;
                vgr.PriceType = voyagerItenaryAREQ.VoyagerInfoArry[0].PriceType;
                vgr.MonthsOfSeasons = voyagerItenaryAREQ.VoyagerInfoArry[0].MonthsOfSeasons;
                vgr.BestTime = voyagerItenaryAREQ.VoyagerInfoArry[0].BestTime;
                vgr.OtherTerms = voyagerItenaryAREQ.VoyagerInfoArry[0].OtherTerms;
                vgr.ActivityType = voyagerItenaryAREQ.VoyagerInfoArry[0].ActivityType;
                vgr.PlaceOfActivity = voyagerItenaryAREQ.VoyagerInfoArry[0].PlaceOfActivity;
                vgr.Grade = voyagerItenaryAREQ.VoyagerInfoArry[0].Grade;
                vgr.StopSell = voyagerItenaryAREQ.VoyagerInfoArry[0].StopSell;
                vgr.Theme = voyagerItenaryAREQ.VoyagerInfoArry[0].Theme;
                vgr.DateCreated = voyagerItenaryAREQ.VoyagerInfoArry[0].DateCreated;
                vgr.ModifiedDate = voyagerItenaryAREQ.VoyagerInfoArry[0].ModifiedDate;
                vgr.MetaTags = voyagerItenaryAREQ.VoyagerInfoArry[0].MetaTags;
                vgr.CanonicalTags = voyagerItenaryAREQ.VoyagerInfoArry[0].CanonicalTags;
                vgr.Keywords = voyagerItenaryAREQ.VoyagerInfoArry[0].Keywords;
                vgr.SocialTags = voyagerItenaryAREQ.VoyagerInfoArry[0].SocialTags;
                vId = vgr.VoyagerID;
                _context.SaveChanges();
                await _context.SaveChangesAsync();
            }
            else
            {
                 _context.Voyager.Add(vg);
                await _context.SaveChangesAsync();
                vId = vg.VoyagerID;
            }







            if (voyagerItenaryAREQ.VoyagerInfoArry[0].Itineraries != null)
            {
                foreach (var itr in voyagerItenaryAREQ.VoyagerInfoArry[0].Itineraries)
                {
                    if (itr != null)
                    {


                        var itn = _context.Itinerary
                                          .FirstOrDefault(x => x.ID == itr.ID && x.VoyagerID == itr.VoyagerID);

                        if (itn != null)
                        {
                            // Update tracked entity
                            itn.ItineraryDesc = itr.ItineraryDesc;
                            itn.DayNo = itr.DayNo;

                            _context.SaveChanges();
                        }
                        else
                        {
                            itr.VoyagerID = vId;
                             _context.Itinerary.Add(itr);
                        }
                    }
                }

                // Persist all changes once
                //await _context.SaveChangesAsync();


            }

            if (voyagerItenaryAREQ.VoyagerInfoArry[0].InclusionsExclusions != null)
            {
                foreach (var iNx in voyagerItenaryAREQ.VoyagerInfoArry[0].InclusionsExclusions)
                {
                    if (iNx != null)
                    {




                        var itn = _context.InclusionsExclusion
                                          .FirstOrDefault(x => x.ID == iNx.ID && x.VoyagerID == iNx.VoyagerID);

                        if (itn != null)
                        {
                            // Update tracked entity
                            itn.Description = iNx.Description;
                            itn.IsInclusions = iNx.IsInclusions;
                            itn.IsActive = iNx.IsActive;

                            _context.SaveChanges();
                        }
                        else
                        {
                            iNx.VoyagerID = vId;
                             _context.InclusionsExclusion.Add(iNx);
                        }

                    }
                }
            }
            if (voyagerItenaryAREQ.VoyagerInfoArry[0].TravelDates != null)
            {
                foreach (var vdt in voyagerItenaryAREQ.VoyagerInfoArry[0].TravelDates)
                {
                    if (vdt != null)
                    {

                        var itn = _context.TravelDate
                                        .FirstOrDefault(x => x.Id == vdt.Id && x.VoyagerID == vdt.VoyagerID);

                        if (itn != null)
                        {
                            // Update tracked entity
                            itn.StartDate = vdt.StartDate;
                            itn.EndDate = vdt.EndDate;
                            itn.NoOfSeats = vdt.NoOfSeats;
                            itn.StopSell = vdt.StopSell;
                            itn.isSeason = vdt.isSeason;
                            itn.Markup = vdt.Markup;
                            itn.IsFixedMarkup = vdt.IsFixedMarkup;

                            _context.SaveChanges();
                        }
                        else
                        {
                            vdt.VoyagerID = vId;
                             _context.TravelDate.Add(vdt);
                        }

                    }
                }
            }
            if (voyagerItenaryAREQ.VoyagerInfoArry[0].Images != null)
            {
                foreach (var img in voyagerItenaryAREQ.VoyagerInfoArry[0].Images)
                {
                    if (img != null)
                    {



                        if (img.Media != null)
                        {
                            var itn = _context.Image
                                     .FirstOrDefault(x => x.ImageID == img.ImageID && x.VoyagerID == img.VoyagerID);
                            if (itn != null)
                            {
                                // Update tracked entity
                                itn.Media = img.Media;
                                itn.ImageDescription = img.ImageDescription;
                                itn.IsBannerImage = img.IsBannerImage;
                                itn.BannerImageSeq = img.BannerImageSeq;
                                itn.IsActive = img.IsActive;
                                itn.ModifiedDate = img.ModifiedDate;
                                itn.ModifiedBy = img.ModifiedBy;

                                _context.SaveChanges();
                            }
                            else
                            {
                                img.VoyagerID = vId;
                                 _context.Image.Add(img);
                            }

                        }

                    }
                }
            }
            if (voyagerItenaryAREQ.VoyagerInfoArry[0].VoyagerFaqs != null)
            {
                foreach (var faq in voyagerItenaryAREQ.VoyagerInfoArry[0].VoyagerFaqs)
                {
                    if (faq != null)
                    {

                        var itn = _context.VoyagerFAQ
                                     .FirstOrDefault(x => x.id == faq.id && x.VoyagerID == faq.VoyagerID);
                        if (itn != null)
                        {
                            // Update tracked entity
                            itn.Question = faq.Question;
                            itn.Answer = faq.Answer;

                            _context.SaveChanges();
                        }
                        else
                        {
                            faq.VoyagerID = vId;
                            _context.VoyagerFAQ.Add(faq);
                        }

                    }
                }
            }
            await _context.SaveChangesAsync();


            voyagerItenaryAREQ.Errors = new Errors { Code = "Err000", Description = "NO Error", Message = "Addition Successfull" };
            return voyagerItenaryAREQ;
        }
        public async Task<LoginRESP> Login(LoginREQ loginREQ)
        {
            LoginRESP loginRESP = new LoginRESP();
            //byte[] encryptedData = EncryptionHelper.ConvertStringToByteArray(loginREQ.LoginReq.LoginPassword);
            int StaffId = 0;
            //string decryptedPassword = await EncryptionHelper.DecryptAsync(encryptedData);
            try
            {
                var stf = _context.LoginInfo
                            .SingleOrDefault(l => l.LoginName == loginREQ.LoginReq.LoginName
                            && l.LoginPassword == loginREQ.LoginReq.LoginPassword);
                loginRESP = new LoginRESP
                {
                    Errors = new Errors { Code = "L0001", Description = "Login Success" },
                    Actions = VoyagerActionType.Login,
                    StaffId = stf.StaffId,
                    Designation = "HOD",
                    StaffName = stf.LoginName,
                    vToken = GenerateToken()
                };

            }
            catch (Exception)
            {

                if (StaffId == 0)
                {
                    LoginRESP loginRESP1 = new LoginRESP
                    {
                        Errors = new Errors { Code = "L0002", Description = "Invalid Credentials" },
                        Actions = VoyagerActionType.Login,
                        StaffId = 0,
                        vToken = string.Empty
                    };
                    return loginRESP1;
                }
            }




            return loginRESP;
        }

        private string GenerateToken()
        {
            // Base from object hash
            string baseHash = this.GetHashCode().ToString();

            // Add randomness with Guid
            string guidPart = Guid.NewGuid().ToString("N"); // 32 chars, no hyphens

            // Combine and trim to 25–30 chars
            string token = (baseHash + guidPart);

            // Ensure length between 25 and 30
            if (token.Length > 30)
                token = token.Substring(0, 30);
            else if (token.Length < 25)
                token = token.PadRight(25, 'X'); // pad with 'X' if too short

            return token;
        }

        public async Task<VoyagerStaticDataRESP> GetStaticData(VoyagerStaticDataREQ REQ)
        {
            bool isSuccess = false;
            var resp = new VoyagerStaticDataRESP();
            if (REQ.RequestFor.ToUpper() == "ACTIVITYTYPE")
            {
                resp = await HandleStaticData(
                                        REQ,
                                        REQ.Action,
                                        _context.ActivityType,
                                        a => new VoyagerStaticData { ID = a.ID, value = a.value },
                                        d => new ActivityType { ID = d.ID, CompanyId = REQ.CompanyId, value = d.value },
                                        (entity, dto) => { entity.value = dto.value; entity.CompanyId = REQ.CompanyId; },
                                        a => a.ID,
                                        a => a.CompanyId
                                    );
                                    
                resp.Action = REQ.Action;
                resp.RequestFor = REQ.RequestFor;
                resp.CompanyId = REQ.CompanyId;

                isSuccess = true;
            }
            if (REQ.RequestFor.ToUpper() == "COUNTRYCODE")
            {
                resp = await HandleStaticData(
                                      REQ,
                                       REQ.Action,
                                       _context.CountryCode,
                                       c => new VoyagerStaticData { ID = c.ID, Code = c.Code, Name = c.Name },
                                       d => new CountryCode { ID = d.ID, CompanyId = REQ.CompanyId, Code = d.Code, Name = d.Name },
                                       (entity, dto) => { entity.Code = dto.Code; entity.Name = dto.Name; entity.CompanyId = REQ.CompanyId; },
                                       c => c.ID,              // idSelector
                                       c => c.CompanyId        // companySelector
                                   );
                resp.Action = REQ.Action;
                resp.RequestFor = REQ.RequestFor;
                resp.CompanyId = REQ.CompanyId;

                isSuccess = true;
            }
            if (REQ.RequestFor.ToUpper() == "CURRENCY")
            {
                 resp= await HandleStaticData(
                                        REQ,
                                        REQ.Action,
                                        _context.Currency,
                                        c => new VoyagerStaticData { ID = c.ID,value="", Code = c.CurrencyCode, Name = c.CurrencyName },
                                        d => new Currency { ID = d.ID, CompanyId = REQ.CompanyId, CurrencyCode = d.Code, CurrencyName = d.Name },
                                        (entity, dto) => { entity.CurrencyCode = dto.Code; entity.CurrencyName = dto.Name; entity.CompanyId = REQ.CompanyId; },
                                        c => c.ID,              // idSelector
                                        c => c.CompanyId        // companySelector
                                    );
                resp.Action = REQ.Action;
                resp.RequestFor = REQ.RequestFor;
                resp.CompanyId = REQ.CompanyId;
               
                isSuccess = true;


            }
            if (REQ.RequestFor.ToUpper() == "THEME")
            {
                resp = await HandleStaticData(
                                       REQ,
                                        REQ.Action,
                                        _context.Theme,
                                        t => new VoyagerStaticData { ID = t.ID, value = t.value },
                                        d => new Theme { ID = d.ID, CompanyId = REQ.CompanyId, value = d.value },
                                        (entity, dto) => { entity.value = dto.value; entity.CompanyId = REQ.CompanyId; },
                                        t => t.ID,              // idSelector
                                        t => t.CompanyId        // companySelector
                                    );
                resp.Action = REQ.Action;
                resp.RequestFor = REQ.RequestFor;
                resp.CompanyId = REQ.CompanyId;

                isSuccess = true;

            }
            if (REQ.RequestFor.ToUpper() == "PRICETYPE")
            {
                resp = await HandleStaticData(
                                       REQ,
                                       REQ.Action,
                                       _context.PriceType,
                                       t => new VoyagerStaticData { ID = t.ID, value = t.value },
                                       d => new PriceType { ID = d.ID, CompanyId = REQ.CompanyId, value = d.value },
                                       (entity, dto) => { entity.value = dto.value; entity.CompanyId = REQ.CompanyId; },
                                       t => t.ID,              // idSelector
                                       t => t.CompanyId        // companySelector
                                   );
                resp.Action = REQ.Action;
                resp.RequestFor = REQ.RequestFor;
                resp.CompanyId = REQ.CompanyId;

                isSuccess = true;
            }
            if (REQ.RequestFor.ToUpper() == "FREQUENCY")
            {
                resp = await HandleStaticData(
                                      REQ,
                                       REQ.Action,
                                       _context.Frequency,
                                       t => new VoyagerStaticData { ID = t.ID, value = t.value },
                                       d => new Frequency { ID = d.ID, CompanyId = REQ.CompanyId, value = d.value },
                                       (entity, dto) => { entity.value = dto.value; entity.CompanyId = REQ.CompanyId; },
                                       t => t.ID,              // idSelector
                                       t => t.CompanyId        // companySelector
                                   );
                resp.Action = REQ.Action;
                resp.RequestFor = REQ.RequestFor;
                resp.CompanyId = REQ.CompanyId;

                isSuccess = true;
            }
            if (REQ.RequestFor.ToUpper() == "DURATIONUNIT")
            {
                resp = await HandleStaticData(
                                      REQ,
                                       REQ.Action,
                                       _context.DurationUnit,
                                       t => new VoyagerStaticData { ID = t.ID, value = t.value },
                                       d => new DurationUnit { ID = d.ID, CompanyId = REQ.CompanyId, value = d.value },
                                       (entity, dto) => { entity.value = dto.value; entity.CompanyId = REQ.CompanyId; },
                                       t => t.ID,              // idSelector
                                       t => t.CompanyId        // companySelector
                                   );
                resp.Action = REQ.Action;
                resp.RequestFor = REQ.RequestFor;
                resp.CompanyId = REQ.CompanyId;
                isSuccess = true;
            }
            if (REQ.RequestFor.ToUpper() == "GRADE")
            {

                resp = await HandleStaticData(
                                      REQ,
                                       REQ.Action,
                                       _context.Grade,
                                       t => new VoyagerStaticData { ID = t.ID, value = t.value },
                                       d => new Grade { ID = d.ID, CompanyId = REQ.CompanyId, value = d.value },
                                       (entity, dto) => { entity.value = dto.value; entity.CompanyId = REQ.CompanyId; },
                                       t => t.ID,              // idSelector
                                       t => t.CompanyId        // companySelector
                                   );
                resp.Action = REQ.Action;
                resp.RequestFor = REQ.RequestFor;
                resp.CompanyId = REQ.CompanyId;
                isSuccess = true;
            }

            if (REQ.RequestFor.ToUpper() == "GROUPTYPE")
            {

                resp = await HandleStaticData(
                                       REQ,
                                       REQ.Action,
                                       _context.GroupType,
                                       t => new VoyagerStaticData { ID = t.ID, value = t.value },
                                       d => new GroupType { ID = d.ID, CompanyId = REQ.CompanyId, value = d.value },
                                       (entity, dto) => { entity.value = dto.value; entity.CompanyId = REQ.CompanyId; },
                                       t => t.ID,              // idSelector
                                       t => t.CompanyId        // companySelector
                                   );
                resp.Action = REQ.Action;
                resp.RequestFor = REQ.RequestFor;
                resp.CompanyId = REQ.CompanyId;
                isSuccess = true;
            }
            // Default response if RequestFor doesn't match
            if(isSuccess)
            {
                
                return resp;
            }
            else
            {
                return new VoyagerStaticDataRESP
                {
                    RequestFor = REQ.RequestFor,
                    CompanyId = REQ.CompanyId,
                    Action = REQ.Action,
                    Data = null,
                    isSuccess = false,
                    Message = "Unsupported RequestFor type"
                };
            }
           
        }




        ////Generic private Method
        ///
        public async Task<VoyagerStaticDataRESP> HandleStaticData<TEntity>(
    VoyagerStaticDataREQ REQ,
    string action,
    DbSet<TEntity> dbSet,
    Func<TEntity, VoyagerStaticData> selector,
    Func<VoyagerStaticData, TEntity> creator,
    Action<TEntity, VoyagerStaticData> updater,
    Func<TEntity, Int64> idSelector,
    Func<TEntity, Int64> companySelector)
    where TEntity : class
        {
            var resp = new VoyagerStaticDataRESP();

            try
            {
                // Build expression: e => e.CompanyId == REQ.CompanyId
                var parameter = Expression.Parameter(typeof(TEntity), "e");
                var companyProp = Expression.Property(parameter, "CompanyId");
                var companyValue = Expression.Constant(REQ.CompanyId);
                var equalExpr = Expression.Equal(companyProp, companyValue);
                var lambda = Expression.Lambda<Func<TEntity, bool>>(equalExpr, parameter);

                switch (action.ToUpperInvariant())
                {
                    case "LIST":
                        var entities = await dbSet.Where(lambda).ToListAsync();
                        resp.Data = entities.Select(selector).ToList();
                        break;

                    case "EDIT":
                        var allData = await dbSet.Where(lambda).ToListAsync();

                        foreach (var item in REQ.Data)
                        {
                            var existing = allData.FirstOrDefault(e => idSelector(e) == item.ID);

                            if (existing != null)
                            {
                                updater(existing, item);
                            }
                            else
                            {
                                dbSet.Add(creator(item));
                            }
                        }

                        await _context.SaveChangesAsync();
                        break;
                    case "DELETE":
                        var toDelete = await dbSet.Where(lambda).ToListAsync();

                        foreach (var item in REQ.Data)
                        {
                            var existing = toDelete.FirstOrDefault(e => idSelector(e) == item.ID);
                            dbSet.Remove(existing);
                            await _context.SaveChangesAsync();
                        }
                           
                        break;
                    default:
                        resp.Message = "Invalid action specified.";
                        break;
                }

                resp.isSuccess = true;
                resp.Message = "Success";
            }
            catch (Exception ex)
            {
                resp.isSuccess = false;
                resp.Message = ex.InnerException?.Message ?? ex.Message;
            }

            return resp;
        }







    }
}