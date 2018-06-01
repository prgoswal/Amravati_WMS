$('input:empty, textarea:empty').closest('label').addClass('empty');

$('input').keyup(function () {
  if ($(this).val().trim() !== '') {
    $(this).closest('label').removeClass('empty');
  } else {
    $(this).closest('label').addClass('empty');
  }
});