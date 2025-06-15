using Microsoft.EntityFrameworkCore;
using Sporty.Enums;
using Sporty.Models;
using Sporty.Repositories;
using Sporty.ViewModels;
namespace Sporty.Services
{
    public class MembershipRequestService
    {
        private readonly DocumentRepository _documentRepository;
        private readonly MembershipRequestRepository _membershipRequestRepository;

        public MembershipRequestService(MembershipRequestRepository membershipRequestRepository , DocumentRepository documentRepository)
        {
            _documentRepository = documentRepository;
            _membershipRequestRepository = membershipRequestRepository;
        }

        private async Task UploadDocuments(IFormCollection form,int RequestID)
        {
           
                var filesMap = new Dictionary<string, string>
              {
                { "EducationalCertificateApplicant", "EducationalCertificateApplicant" }
                
              };


                var uploadRoot = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "membership_requests", RequestID.ToString());
                Directory.CreateDirectory(uploadRoot); // Ensure directory exists

                foreach (var key in filesMap.Keys)
                {
                    var files = form.Files.GetFiles(key);
                    foreach (var file in files)
                    {
                        if (file.Length > 0)
                        {
                            var uniqueFileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                            var savePath = Path.Combine(uploadRoot, uniqueFileName);

                            using (var stream = new FileStream(savePath, FileMode.Create))
                            {
                                await file.CopyToAsync(stream);
                            }

                            var document = new Document
                            {
                                DocumentType = filesMap[key],
                                FilePath = savePath,
                                UploadedAt = DateTime.Now,
                                RequestID = RequestID,
                                IsValidated = false
                            };

                            await _documentRepository.AddDocument(document);
                        }
                    }
                }

                await _documentRepository.save();
                //return RedirectToAction("Details", new { id = requestId });
           


        }


        public async Task AddRequest(IFormCollection form)
        {
            try
            {
                var MembershipRequest = new MembershipRequests
                {
                    UserId = form["UserId"],
                    BranchId = int.Parse(form["BranchId"]),
                    ClubId = int.Parse(form["ClubId"]),
                    MembeshipTypeId = int.Parse(form["MembershipTypeId"])
                };
                await _membershipRequestRepository.AddRequest(MembershipRequest);
                int RequestID = MembershipRequest.Id;
                await UploadDocuments(form,RequestID);

            }

            catch (Exception ex) { throw ex; }
            

        }
    
        
        public async Task<List<MembershipRequests>> GetRequestsByUserId(string userId)
        {
            try
            {
                return  await _membershipRequestRepository.GetAllRequests(userId);
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving membership requests", ex);
            }
        }

        public async Task<List<MembershipRequests>> GetRequestsByClubId(int Clubid)
        {
            try
            {
                return await _membershipRequestRepository.GetAllRequests(Clubid);
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving membership requests", ex);
            }
        }

        public async Task<MembershipRequests> GetRequest(int RequestID)
        {
            return await _membershipRequestRepository.GetRequestById(RequestID);
        }
        public MembershipRequests GetRequestSync(int RequestID)
        {
            return _membershipRequestRepository.GetRequestByIdSync(RequestID);
        }

        public async Task<MembershipRequests> GetRequestWithDocuments(int RequestID)
        {
            return await _membershipRequestRepository.GetRequestWithDoc(RequestID);
        }

        public void TakeDecision(MembershipRequestUpdateViewModel model)
        {
            var request =  GetRequestSync(model.Id);
            if (request == null)
                throw new Exception($"Membership request with ID {model.Id} not found."); // Handle null reference
            
            if (model.Decision == 1) { request.Status = MembershipRequestStatus.Accepted; }
            else if(model.Decision==2) { request.Status = MembershipRequestStatus.Rejected; }

            if (model.Decision != 3) { request.DecisionDate = DateTime.Now; } // Optionally set a decision date
                request.Notes = model.Notes;
                request.InterviewRequired = model.InterviewRequired;
                request.InterviewDate = model.InterviewDate;
                
            try
            {
                 _membershipRequestRepository.UpdateRequest(request);
            }
            catch(Exception ex) { throw ex; }
        }

    }
}
