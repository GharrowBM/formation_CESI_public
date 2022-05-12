liste_notes = []
note_entree = 0
nombre_notes = 0

while note_entree >= 0:
    note_entree = int(input("Veuilliez entrer une note entre 0 et 20 compris (une note négative stoppera la saisie) : "))
    if note_entree >= 0:
        nombre_notes += 1
        liste_notes.append(note_entree)

print(f"La note maximale est de {max(liste_notes):0.2f} / 20")
print(f"La note minimale est de {min(liste_notes):0.2f} / 20")
print(f"La somme des notes est de {sum(liste_notes):0.2f}")
print(f"La moyenne est de {sum(liste_notes) / nombre_notes:0.2f} / 20")