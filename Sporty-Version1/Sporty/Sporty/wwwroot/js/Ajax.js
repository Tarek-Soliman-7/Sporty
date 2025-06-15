//inpute 
$(document).ready(function () {
    $('#ClubDropdown').change(function () {
        var clubId = $(this).val();
        if (clubId) {
            $.getJSON('/Event/GetBranchesByClub', { clubId: clubId }, function (data) {
                var branchDropdown = $('#BranchDropdown');
                branchDropdown.empty();
                branchDropdown.append($('<option>').text('اختر فرع').attr('value', ''));
                $.each(data, function (i, branch) {
                    branchDropdown.append($('<option>').text(branch.name).attr('value', branch.id));
                });
            });
        } else {
            $('#BranchDropdown').empty().append($('<option>').text('اختر فرع').attr('value', ''));
        }
    });
});

//search list
    function filterByClub() {
        var clubId = document.getElementById("ClubDropdown").value;
    if (clubId) {
        window.location.href = '/Event/Index?clubId=' + clubId;
        } else {
        window.location.href = '/Event/Index';
        }
    }

