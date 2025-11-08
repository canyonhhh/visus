class CreateQrCodes < ActiveRecord::Migration[8.1]
  def change
    create_table :qr_codes do |t|
      t.references :activity, null: false, foreign_key: true
      t.string :token
      t.boolean :is_active

      t.timestamps
    end

    add_index :qr_codes, :token, unique: true
    change_column_default :qr_codes, :is_active, from: nil, to: true
  end
end
