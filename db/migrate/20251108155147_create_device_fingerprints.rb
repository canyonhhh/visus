class CreateDeviceFingerprints < ActiveRecord::Migration[8.1]
  def change
    create_table :device_fingerprints do |t|
      t.references :student, null: false, foreign_key: true
      t.string :fingerprint_value
      t.datetime :last_seen_at

      t.timestamps
    end

    add_index :device_fingerprints, :fingerprint_value, unique: true
  end
end
