// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
<script>
    $(document).ready(function () {
        $('#ClubDropdown').change(function () {
            var clubId = $(this).val();
            if (clubId) {
                $.getJSON('/Event/GetBranchesByClub', { clubId: clubId }, function (data) {
                    var branchDropdown = $('#BranchDropdown');
                    branchDropdown.empty();
                    branchDropdown.append($('<option>').text('Choose').attr('value', ''));
                    $.each(data, function (i, branch) {
                        branchDropdown.append($('<option>').text(branch.name).attr('value', branch.id));
                    });
                });
            } else {
                $('#BranchDropdown').empty().append($('<option>').text('Choose').attr('value', ''));
            }
        });
    });
</script>
