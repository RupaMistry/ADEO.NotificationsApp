import {
    ClassicEditor, AccessibilityHelp, Alignment, Autoformat, AutoLink, Autosave,
    BlockQuote, Bold, Code, CodeBlock, Essentials, FindAndReplace, FontBackgroundColor,
    FontColor, FontFamily, FontSize, FullPage, GeneralHtmlSupport, Heading, Highlight,
    HorizontalLine, HtmlComment, HtmlEmbed, Indent, IndentBlock, Italic, Link, Paragraph, RemoveFormat,
    SelectAll, ShowBlocks, SourceEditing, SpecialCharacters, SpecialCharactersArrows, SpecialCharactersCurrency,
    SpecialCharactersEssentials, SpecialCharactersLatin, SpecialCharactersMathematical, SpecialCharactersText,
    Strikethrough, Subscript, Superscript, Table, TableCaption, TableCellProperties, TableColumnResize,
    TableProperties, TableToolbar, TextTransformation, Underline, Undo
} from 'ckeditor5';

let editor;
let isEditMessage;

const editorConfig = {
    toolbar: {
        items: ['undo', 'redo', '|', 'sourceEditing', 'showBlocks', 'findAndReplace', 'selectAll', '|', 'heading', '|', 'fontSize', 'fontFamily', 'fontColor',
            'fontBackgroundColor', '|', 'bold', 'italic', 'underline', 'strikethrough', 'subscript', 'superscript', 'code', 'removeFormat', '|', 'specialCharacters',
            'horizontalLine', 'link', 'insertTable', 'highlight', 'blockQuote', 'codeBlock', 'htmlEmbed', '|', 'alignment', '|', 'outdent', 'indent', '|', 'accessibilityHelp'],
        shouldNotGroupWhenFull: false
    },
    plugins: [
        AccessibilityHelp,
        Alignment,
        Autoformat,
        AutoLink,
        Autosave,
        BlockQuote,
        Bold,
        Code,
        CodeBlock,
        Essentials,
        FindAndReplace,
        FontBackgroundColor,
        FontColor,
        FontFamily,
        FontSize,
        FullPage,
        GeneralHtmlSupport,
        Heading,
        Highlight,
        HorizontalLine,
        HtmlComment,
        HtmlEmbed,
        Indent,
        IndentBlock,
        Italic,
        Link,
        Paragraph,
        RemoveFormat,
        SelectAll,
        ShowBlocks,
        SourceEditing,
        SpecialCharacters,
        SpecialCharactersArrows,
        SpecialCharactersCurrency,
        SpecialCharactersEssentials,
        SpecialCharactersLatin,
        SpecialCharactersMathematical,
        SpecialCharactersText,
        Strikethrough,
        Subscript,
        Superscript,
        Table,
        TableCaption,
        TableCellProperties,
        TableColumnResize,
        TableProperties,
        TableToolbar,
        TextTransformation,
        Underline,
        Undo
    ],
    fontFamily: {
        supportAllValues: true
    },
    fontSize: {
        options: [10, 12, 14, 'default', 18, 20, 22],
        supportAllValues: true
    },
    heading: {
        options: [
            {
                model: 'paragraph',
                title: 'Paragraph',
                class: 'ck-heading_paragraph'
            },
            {
                model: 'heading1',
                view: 'h1',
                title: 'Heading 1',
                class: 'ck-heading_heading1'
            },
            {
                model: 'heading2',
                view: 'h2',
                title: 'Heading 2',
                class: 'ck-heading_heading2'
            },
            {
                model: 'heading3',
                view: 'h3',
                title: 'Heading 3',
                class: 'ck-heading_heading3'
            },
            {
                model: 'heading4',
                view: 'h4',
                title: 'Heading 4',
                class: 'ck-heading_heading4'
            },
            {
                model: 'heading5',
                view: 'h5',
                title: 'Heading 5',
                class: 'ck-heading_heading5'
            },
            {
                model: 'heading6',
                view: 'h6',
                title: 'Heading 6',
                class: 'ck-heading_heading6'
            }
        ]
    },
    htmlSupport: {
        allow: [
            {
                name: /^.*$/,
                styles: true,
                attributes: true,
                classes: true
            }
        ]
    },
    link: {
        addTargetToExternalLinks: true,
        defaultProtocol: 'https://',
        decorators: {
            toggleDownloadable: {
                mode: 'manual',
                label: 'Downloadable',
                attributes: {
                    download: 'file'
                }
            }
        }
    },
    menuBar: {
        isVisible: true
    },
    placeholder: 'Type or paste your message content here!',
    table: {
        contentToolbar: ['tableColumn', 'tableRow', 'mergeTableCells', 'tableProperties', 'tableCellProperties']
    },
};

ClassicEditor.create(document.querySelector('#editor'), editorConfig)
    .then(newEditor => {
        editor = newEditor;
    })
    .catch(error => {
        console.error(error);
    });

document.querySelector('#btnSubmit').addEventListener('click', () => {
     
    createEditMessage();
});

document.querySelector('#btnSentScreen').addEventListener('click', () => {

    const content = editor.getData();

    var messageID = localStorage.getItem("messageID");

    var url = "Notifications/PublishMessage?messageID=" + messageID; 
    var jqxhr = $.post(url, function () { })
        .done(function (response) { 
            if (response >= 0) {

                // show toast ppopup.
                toastr["success"]("Notification sent to screen successfully!");

                resetState();
            }
        })
        .fail(function (error) {
            $('#btnMessageClose').click();
            toastr["error"](error);
        })
});

$(document).ready(() => {  
    $('#btnMessageEditor').on("click", function () {

        setupPopup(-1, false);
    });
    $('.editMessage').on("click", function () {
         
        const messageID = $(this).attr('messageID');
         
        setupPopup(messageID, false);

        isEditMessage = true;
    });

    $('.publish').on("click", function () {

        const messageID = $(this).attr('messageID');
         
        setupPopup(messageID, true); 
    });

    $('.copyRepublish').on("click", function () {
        debugger;
        const messageID = $(this).attr('messageID');

        isEditMessage = false;
        setupPopup(messageID, false);

        // Set message date to null for user to select when they want to publish
        $('#msgDate').val('');
    });

    $('.endPublish').on("click", function () {

        const messageID = $(this).attr('messageID');

        if (messageID <= 0) {
            return;
        }

        var url = "Notifications/EndPublishMessage?messageID=" + messageID;

        var jqxhr = $.post(url, function () { })
            .done(function (response) {

                if (response >= 0) {

                    // show toast ppopup.
                    toastr["success"]("End published successfully!");

                    resetState();
                }
            })
            .fail(function (error) { 
                $('#btnMessageClose').click(); 
                toastr["error"](error);
            })
    });
});

function createEditMessage() {
    const content = editor.getData();

    var messageID = localStorage.getItem("messageID");

    if (!isEditMessage) {
        createNewMessage($('#msgDate').val(), content);
    }
    else {
        editNewMessage(messageID, $('#msgDate').val(), content);
    }
}

function setupPopup(messageID, isPublish) {
    debugger;
    if (isPublish) {

        $("#divPopupContent *").prop('disabled', true);
        editor.enableReadOnlyMode('true');

        $('#btnSubmit').hide();
        $('#btnSentScreen').show();
    }
    else {
        editor.disableReadOnlyMode('true');

        $('#btnSentScreen').hide();
        $('#btnSubmit').show();
    }

    if (messageID <= 0) {
        $('#msgDate').val('');
        editor.setData('');

        return;
    }

    editor.setData($('#txtMsgContent_' + messageID).text());
    $('#msgDate').val($('#txtMsgDate_' + messageID).text());

    localStorage.setItem("messageID", messageID);
}

function editNewMessage(messageId, messageDate, content) {

    var url = "Notifications/EditMessage";

    var userMessage =
    {
        "ID": messageId,
        "MessageDate": messageDate,
        "Content": content,
    }

    var jqxhr = $.post(url, userMessage, function () { })
        .done(function (response) {

            if (response >= 0) {

                resetState();

                // show toast ppopup.
                toastr["success"]("Notification message updated successfully!"); 
            }
        })
        .fail(function (error) {
            $('#btnMessageClose').click();
            toastr["error"](error);
        })
}

function createNewMessage(messageDate, content) {

    var url = "Notifications/CreateNewMessage"; 

    var userMessage =
    {
        "MessageDate": messageDate,
        "Content": content,
    }
    var jqxhr = $.post(url, userMessage, function () { })
        .done(function (response) {

            if (response >= 0) {

                resetState();

                // show toast ppopup.
                toastr["success"]("Notification created successfully!");
            }
        })
        .fail(function (error) {
            $('#btnMessageClose').click();
            toastr["error"](error);
        })
}

function resetState() {

    // Clear form contents and close popup.
    $('#msgDate').val('');
    editor.setData('');
    $('#btnMessageClose').click();

    window.location.reload();

    localStorage.clear();
}