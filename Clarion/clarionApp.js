// JavaScript source code

var chat; // hey why not
var statsId; // id of currently-looking-at combatant
$(function () {
    // Declare a proxy to reference the hub.
    chat = $.connection.chatHub;
    // Create a function that the hub can call to broadcast messages.
    chat.client.broadcastMessage = function (message) {
        // Html encode display name and message.
        var encodedMsg = $('<div />').text(message).html();
        // Add the message to the page.
        $('#discussion').append('<li>'+ encodedMsg + '</li>');
        $('#discussion').scrollTop($('#discussion')[0].scrollHeight);
    };

    // Client function for fate update (soft) - need one for setting initial state
    chat.client.updateFates = function (r) {
        nextCard(r);
    };

    chat.client.setFates = function (f, r) {
        $("div.card").first().html(f);
        $("div.card:nth-of-type(2)").first().html(r);}

    // Client function for adding combatants
    chat.client.addCombatant = function (charId, charName, targetArea) {
        newCombatant(charId, charName, targetArea);
    };

    chat.client.addCombatant = function(charId, charName, targetArea) {
        var newNode = document.createElement("LI");
        var newText = document.createTextNode(charName);
        newNode.appendChild(newText);
        newNode.id = charId;
        document.getElementById(targetArea).firstElementChild.appendChild(newNode);
        prepareLi(charId);
    }


    function nextCard(cardnum) {
        var newcard = "<div class='card'>" + cardnum + "</div>";
        $("div.cardContainer").append(newcard);
        $("div.card").first().slideUp('fast', function () {
            $("div.card").first().remove();
        });
    };


    chat.client.newPlayer = function (payload) {

        console.log($.parseJSON(payload));
    };

    
    $.connection.hub.start();

    chat.client.moveById = function (charId, targetArea) {
        var element = document.getElementById(charId);
        var target = document.getElementById(targetArea);
        target.firstElementChild.appendChild(element);
    }

    chat.client.deleteById = function (charId) {
        var element = document.getElementById(charId);
        element.parentElement.removeChild(element);
    }

    chat.client.updateStats = function (life, hp, mp, bravery) {
        updateStats(life, hp, mp, bravery);}

    $("ul.area-contents li").attr("draggable", "true").attr("ondragstart", "areaEntryDrag(event)");
    $("ul.area-contents li").click(function () { clickget(event) });
    $("ul.area-contents").attr("droppable", "true").attr("ondragover", "allowDrop(event)").attr("ondrop", "drop(event)");
    $("div.delete-area").attr("droppable", "true").attr("ondragover", "allowDrop(event)").attr("ondrop", "deletedrop(event)");
    $("button#addCombatant").click(function () { addCombatant($("input#nameInput").val(), 1); })
    $("input.barInner").change(function () { pushStats(); });
    $("div.cardWindow").dblclick(function () { chat.server.turn();})
});

function prepareLi(id) {
    $("ul.area-contents li#" + id).attr("draggable", "true").attr("ondragstart", "areaEntryDrag(event)");
    $("ul.area-contents li#" + id).click( function() {clickget(event)});
}

function areaEntryDrag(event) {
    var sourceArea = event.target.parentElement.parentElement.id;
    event.dataTransfer.setData("sourceArea", sourceArea);
    event.dataTransfer.setData("taskItem", event.target.id);

}

function drop(ev) {
    ev.preventDefault();
    var data = ev.dataTransfer.getData("taskItem");
    var sourceArea = ev.dataTransfer.getData("sourceArea")
    var element = document.getElementById(data);
    var target = ev.target;
    if (target.tagName == "LI") {
        target = target.parentElement;
    }
    var targetArea = target.parentElement.id;
    target.appendChild(element);
    chat.server.move(data, targetArea);
}

function deletedrop(ev) {
    ev.preventDefault();
    var data = ev.dataTransfer.getData("taskItem");
    deleteById(data);}

function clickget(ev) {
    var data = ev.target.id;
    statsId = data;
    chat.server.fetchStats(data);}

function allowDrop(ev) {
    ev.preventDefault();
}

function moveById(charId, targetArea) {
    var element = document.getElementById(charId);
    var target = document.getElementById(targetArea);
    target.firstElementChild.appendChild(element);
}

function deleteById(charId) {
    var element = document.getElementById(charId);
    element.parentElement.removeChild(element);
    chat.server.removeCombatant(charId);
}

function addCombatant(charName, targetArea) {
    chat.server.addCombatant(charName, targetArea);
}

function updateStats(life, hp, mp, bravery) {
    $("input#LifeInner").val(life);
    $("input#HPInner").val(hp);
    $("input#MPInner").val(mp);
    $("input#BraveInner").val(bravery);
}

function pushStats() {
    if (statsId != null) {
        var life, hp, mp, brave;
        life = $("input#LifeInner").val();
        hp = $("input#HPInner").val();
        mp = $("input#MPInner").val();
        brave = $("input#BraveInner").val();
        console.log(life + "," + hp + "," + mp + "," + brave);
        chat.server.pushStats(statsId,life,hp,mp,brave);
    }
}
